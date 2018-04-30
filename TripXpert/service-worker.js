var cacheName = 'v1';
var cachedFiles = [
    '/',
    '/Content/bootstrap.css',
    '/Content/TripX.css',
    '/Scripts/modernizr-2.6.2.js',
    '/Content/kendo/2018.1.221/kendo.common-bootstrap.min.css',
    '/Content/kendo/2018.1.221/kendo.bootstrap.min.css',
    '/Scripts/jquery-3.1.1.min.js',
    '/Scripts/kendo/2018.1.221/kendo.custom.min.js',
    '/Scripts/TripXpert.js',
    'https://kendo.cdn.telerik.com/2018.1.221/styles/kendo.nova.mobile.min.css',
    '/Images/App/trip_xpert_logo.png',
    '/Content/kendo/2018.1.221/fonts/glyphs/WebComponentsIcons.ttf',
    '/Home/GetDestinations',
    '/Home/GetSpecialDestinations',
    '/Content/kendo/2018.1.221/fonts/glyphs/WebComponentsIcons.ttf?gedxeo',
    'https://kendo.cdn.telerik.com/2018.1.221/styles/images/kendoui.woff?v=1.1',
    '/images/gallery/Barcelona%20and%20Tenerife/138x138/Barcelona-Fish_Liliya-Karakoleva_Attraction.JPG',
    '/images/gallery/United%20States%20-%20East%20Coast%20tour%201/138x138/Fenway-Park_Attraction.jpg',
    '/images/gallery/Malta/138x138/Church-of-St.-Catherine-of-Alexandria_Maria-Kovacheva_Attraction.jpg',
    '/images/gallery/Wonders%20of%20Italy/138x138/Duomo-of-Florence2_Lilia-Karakoleva_Attraction.jpg',
    '/images/gallery/The%20Best%20of%20Western%20Europe/138x138/Frankfurt-Bridge_Daniel-Peichev_Attraction.jpg',
    '/images/gallery/UNESCO%20sites%20in%20Bulgaria/138x138/Alexander-Nevsky-Cathedral_Svetlin-Nikolaev_Attraction.jpg',
    '/images/gallery/India/138x138/Amber-Fort_Attraction.jpg',
    '/images/gallery/Highlights%20of%20South%20Africa/138x138/Featherbed-Nature-Reserve_Attraction.jpg',
    '/images/gallery/France%20-%20A%20Grand%20Tour/138x138/Arc-de-Triomphe_Attraction.jpg',
    '/images/gallery/Costa%20Rican%20Treasures/138x138/Barra-Honda-National-Park_Daniel-Peichev_Attraction.JPG',
    '/images/gallery/Brazil%20and%20Argentina%20Iguazu%20Falls/138x138/Cristo-Redentor-(Christ-the-Redeemer)_Galabina-Yordanova_Attraction.JPG',
    '/images/gallery/Cuba/138x138/Cienfuegos2_Kiril-Nedyalkov_Attraction.jpg',
    '/images/gallery/Egypt%20Explorer/138x138/Abu-Simbel-Temples_Dimo-Dimov_Attraction.jpg',
    '/images/gallery/England,%20Scotland%20and%20Wales/138x138/Big-Ben_Valentin-Likyov_Attraction.jpg',
    '/images/gallery/Golden%20Myanmar/138x138/Mahamuni-Pagoda_Attraction.jpg',
    '/images/gallery/Imperial%20China/138x138/Forbidden-City_Attraction.jpg',
    '/images/gallery/Kenya/138x138/Great-Rift-Valley_Attraction.jpg',
    '/images/gallery/New%20York%20City%20Tour/138x138/Brooklyn-Bridge_Pavlina-Hadjieva_Attraction.JPG',
    '/images/gallery/Spectacular%20Highlights%20of%20Turkey%20and%20Greece/138x138/Blue-Mosque_Valentin-Likyov_Attraction.jpg',
    '/images/gallery/The%20best%20of%20Japan/138x138/Hiroshima-Peace-Memorial,-Genbaku-Dome_Ivan_Zhekov_Attraction.jpg',
    '/images/gallery/The%20Best%20of%20Central%20Europe/138x138/Berlin-Wall_Liliya-Karakoleva_Attraction.JPG',
    '/images/gallery/Classic%20Tour%20of%20Peru/138x138/Cathedral-of-Lima_Attraction.jpg',
    '/images/gallery/Grand%20Tour%20of%20Italy/138x138/Doges-Palace-Venice_Lilia-Karakoleva_Attraction.jpg',
    '/images/gallery/France%20-%20A%20Grand%20Tour/2000x1125/Eiffel-Tower_Attraction.jpg',
    '/images/gallery/Brazil%20and%20Argentina%20Iguazu%20Falls/2000x1125/Cristo-Redentor-(Christ-the-Redeemer)_Galabina-Yordanova_Attraction.JPG',
    '/images/gallery/Classic%20Tour%20of%20Peru/2000x1125/Titicaca-Lake_Attraction.jpg',
    '/images/gallery/Egypt%20Explorer/2000x1125/Temple-of-Isis_Dimo-Dimov_Attraction.jpg',
    '/images/gallery/The%20best%20of%20Japan/2000x1125/Kinkaku-ji-Temple_Ivan-zhekov_Attraction.JPG',
    '/images/gallery/Golden%20Myanmar/2000x1125/The-White-Whale-Temple_Attraction.jpg'
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
