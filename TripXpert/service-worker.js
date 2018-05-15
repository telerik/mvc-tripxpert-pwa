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
    '/Images/Gallery/Barcelona-and-Tenerife/Arc-de-Triomf,-Barcelona,-Spain_Liliya-Karakoleva.JPG?width=320&height=320',
    '/Images/Gallery/United-States/Boston-Old-South-Church_Ivo-Igov.JPG?width=320&height=320',
    '/Images/Gallery/Malta/Bibliotheca-National-Library_Marie-Lan-Nguyen.JPG?width=320&height=320',
    '/Images/Gallery/Barcelona-and-Tenerife/Arc-de-Triomf,-Barcelona,-Spain_Liliya-Karakoleva.JPG?width=450&height=450',
    '/Images/Gallery/United-States/Boston-Old-South-Church_Ivo-Igov.JPG?width=450&height=450',
    '/Images/Gallery/Malta/Bibliotheca-National-Library_Marie-Lan-Nguyen.JPG?width=450&height=450',
    '/Images/Gallery/Italy/Basilica-di-San-Pietro-in-Vaticano2_Lilia-Karakoleva.jpg?width=450&height=450',
    '/Images/Gallery/Western-Europe/Austrian-Parliament,-Vienna,-Austria_Gergana-Prokopieva.jpg?width=450&height=450',
    '/Images/Gallery/UNESCO-Bulgaria/Ahmed-Bay-Mosque,-Kustendil,-Bulgaria_Svetlin-Nikolaev.jpg?width=450&height=450',
    '/Images/Gallery/India/Amber-Fort_Attraction.jpg?width=450&height=450',
    '/Images/Gallery/South-Africa/Featherbed-Nature-Reserve_Attraction.jpg?width=450&height=450',
    '/Images/Gallery/France/Arc-de-Triomphe_Attraction.jpg?width=450&height=450',
    '/Images/Gallery/Costa-Rica/Barra-Honda-National-Park_Daniel-Peichev_Attraction.JPG?width=450&height=450',
    '/Images/Gallery/Brazil-and-Argentina/Aconcagua-national-park-Confluencia-camp2_Galabina-Yordanova.JPG?width=450&height=450',
    '/Images/Gallery/Cuba/Beach_Kiril-Nedyalkov.jpg?width=450&height=450',
    '/Images/Gallery/Egypt/Abu-Simbel-Temples_Dimo-Dimov_Attraction.jpg?width=450&height=450',
    '/Images/Gallery/England-Scotland-and-Wales/Bath,-United-Kingdom_Gergana-Prokopieva.JPG?width=450&height=450',
    '/Images/Gallery/Myanmar/Mahamuni-Pagoda_Attraction.jpg?width=450&height=450',
    '/Images/Gallery/China/Detail-of-bronze-lion-inside-Forbidden-City-in-Beijing,-China.jpg?width=450&height=450',
    '/Images/Gallery/Kenya/Giraffe_Herd_in_Field_Kenya_Africa_Ivan-Zhekov.JPG?width=450&height=450',
    '/Images/Gallery/New-York-City/Brooklyn-Bridge-View_Pavlina-Hadjieva.JPG?width=450&height=450',
    '/Images/Gallery/Turkey-and-Greece/Blue-Mosque,-Istanbul,-Turkey2_Valentin-Likyov.jpg?width=450&height=450',
    '/Images/Gallery/Japan/Fuji-san3_Ivan-Zhekov.JPG?width=450&height=450',
    '/Images/Gallery/Central-Europe/Berlin-Cathedral,-Berlin,-Germany4.jpg?width=450&height=450',
    '/Images/Gallery/Peru/Beach-Zorritos,-Peru_Svetlin-Nikolaev.JPG?width=450&height=450',
    '/Images/Gallery/Barcelona-and-Tenerife/Arc-de-Triomf,-Barcelona,-Spain_Liliya-Karakoleva.JPG?width=450&height=450',
    '/Images/Gallery/United-States/Boston-Old-South-Church_Ivo-Igov.JPG?width=450&height=450',
    '/Images/Gallery/Malta/Bibliotheca-National-Library_Marie-Lan-Nguyen.JPG?width=450&height=450'
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
