var cacheName = 'v1';
var cachedFiles = [
    '/',
    '/Content/bootstrap.css',
    '/Content/kendo-theme.css',
    '/Content/tripxpert.css',
    '/Scripts/modernizr-2.8.3.js',
    '/Scripts/jquery-3.1.1.min.js',
    'https://kendo.cdn.telerik.com/2018.1.221/js/kendo.all.min.js',
    'https://kendo.cdn.telerik.com/2018.1.221/js/kendo.aspnetmvc.min.js',
    '/Scripts/tripxpert.js',
    '/Images/App/tripxpert_logo.svg',
    '/Home/GetDestinations',
    '/Home/GetSpecialDestinations',
    '/Destinations/Destinations_First',
    '/Home/GetPhotoInCertainSize?path=/Images/Gallery/Barcelona-and-Tenerife/Arc-de-Triomf,-Barcelona,-Spain_Liliya-Karakoleva.JPG&width=320&height=320',
    '/Home/GetPhotoInCertainSize?path=/Images/Gallery/United-States/Boston-Old-South-Church_Ivo-Igov.JPG&width=320&height=320',
    '/Home/GetPhotoInCertainSize?path=/Images/Gallery/Malta/Bibliotheca-National-Library_Marie-Lan-Nguyen.JPG&width=320&height=320'
];

var getPath = function (href) {
    var l = new URL(href);
    return l.pathname;
};

self.addEventListener('install', function (e) {

    e.waitUntil(
        caches.open(cacheName).then(function (cache) {
            return cache.addAll(cachedFiles);
        })
    );
});

self.addEventListener('activate', function (e) {

    e.waitUntil(
        caches.keys().then(function (cachedNames) {
            cachedNames.forEach(function (currentCache) {
                if (currentCache !== cacheName) {
                    caches.delete(currentCache);
                }
            }, this);
        })
    );
});

self.addEventListener('fetch', function (e) {
    var urlHelper = new URL(e.request.url);
    if (cachedFiles.indexOf(e.request.url) !== -1 ||
        cachedFiles.indexOf(urlHelper.pathname + urlHelper.search) !== -1
        ) {
        e.respondWith(
            caches.match(e.request).then(function (resp) {
                return resp || fetch(e.request).then(function (response) {
                    caches.open('v1').then(function (cache) {
                        cache.put(e.request, response.clone());
                    });
                    return response;
                });
            }).catch(function () {
                return caches.match('/');
            })
        );
    }
});
