var cacheName = 'v1';
var cachedFiles = [
    '/',
    '/Content/bootstrap.css',
    '/Content/kendo-theme.css',
    '/Content/tripxpert.css',
    '/Scripts/modernizr-2.8.3.js',
    '/Scripts/jquery-3.1.1.min.js',
    '/Scripts/kendo/2018.1.221/kendo.custom.min.js',
    '/Scripts/tripxpert.js',
    '/Images/App/trip_xpert_logo.png',
    '/Home/GetDestinations',
    '/Home/GetSpecialDestinations',
    '/Images/Gallery/United-States/Fenway-Park_Attraction.jpg',
    '/Images/Gallery/Malta/Church-of-St.-Catherine-of-Alexandria_Maria-Kovacheva_Attraction.jpg',
    '/Images/Gallery/Western-Europe/Frankfurt-Bridge_Daniel-Peichev_Attraction.jpg',
    '/Images/Gallery/UNESCO-Bulgaria/Alexander-Nevsky-Cathedral_Svetlin-Nikolaev_Attraction.jpg',
    '/Images/Gallery/India/Amber-Fort_Attraction.jpg',
    '/Images/Gallery/South-Africa/Featherbed-Nature-Reserve_Attraction.jpg',
    '/Images/Gallery/France/Arc-de-Triomphe_Attraction.jpg',
    '/Images/Gallery/Costa-Rica/Barra-Honda-National-Park_Daniel-Peichev_Attraction.JPG',
    '/Images/Gallery/Brazil-and-Argentina/Cristo-Redentor-(Christ-the-Redeemer)_Galabina-Yordanova_Attraction.JPG',
    '/Images/Gallery/Cuba/Cienfuegos2_Kiril-Nedyalkov_Attraction.jpg',
    '/Images/Gallery/Egypt/Abu-Simbel-Temples_Dimo-Dimov_Attraction.jpg',
    '/Images/Gallery/England-Scotland-and-Wales/Big-Ben_Valentin-Likyov_Attraction.jpg',
    '/Images/Gallery/Myanmar/Mahamuni-Pagoda_Attraction.jpg',
    '/Images/Gallery/China/Forbidden-City_Attraction.jpg',
    '/Images/Gallery/Kenya/Great-Rift-Valley_Attraction.jpg',
    '/Images/Gallery/New-York-City/Brooklyn-Bridge_Pavlina-Hadjieva_Attraction.JPG',
    '/Images/Gallery/Turkey-and-Greece/Blue-Mosque_Valentin-Likyov_Attraction.jpg',
    '/Images/Gallery/Central-Europe/Berlin-Wall_Liliya-Karakoleva_Attraction.JPG',
    '/Images/Gallery/Peru/Cathedral-of-Lima_Attraction.jpg',
    '/Images/Gallery/Italy/Doges-Palace-Venice_Lilia-Karakoleva_Attraction.jpg'
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
    if (cachedFiles.indexOf(e.request.url) !== -1 || cachedFiles.indexOf(getPath(e.request.url)) !== -1) {
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
