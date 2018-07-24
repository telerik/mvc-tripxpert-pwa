var cacheName = 'v1';
var url = self.location.href.split("/");

url.pop();

var path = url.join("/") + "/"

var cachedFiles = [
    path,
    path + "bundles/css?v=9bf_1hN1hPPO3xXNTcN-zh4IFDRFrfVrnaYTdddcBHQ1",
    path + "bundles/js?v=3NJpYh6kMRRulVfyFMXyChUMveyhk6-RqaWX_5pxkQY1",
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.core.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.data.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.aspnetmvc.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.popup.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.menu.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.listview.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.list.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.combobox.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.button.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.dropdownlist.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.userevents.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.draganddrop.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.slider.min.js',
    'https://kendo.cdn.telerik.com/2018.2.516/js/kendo.responsivepanel.min.js ',
    path + 'Images/App/tripxpert_logo.svg',
    path + 'Home/GetDestinations',
    path + 'Home/GetSpecialDestinations',
    path + 'Destinations/Destinations_First',
    path + 'Images/Gallery/Barcelona-and-Tenerife/Arc-de-Triomf,-Barcelona,-Spain_Liliya-Karakoleva.JPG?width=320&height=320',
    path + 'Images/Gallery/United-States/Boston-Old-South-Church_Ivo-Igov.JPG?width=320&height=320',
    path + 'Images/Gallery/Malta/Bibliotheca-National-Library_Marie-Lan-Nguyen.JPG?width=320&height=320',
    path + 'Images/Gallery/Barcelona-and-Tenerife/Arc-de-Triomf,-Barcelona,-Spain_Liliya-Karakoleva.JPG?width=450&height=450',
    path + 'Images/Gallery/United-States/Boston-Old-South-Church_Ivo-Igov.JPG?width=450&height=450',
    path + 'Images/Gallery/Malta/Bibliotheca-National-Library_Marie-Lan-Nguyen.JPG?width=450&height=450',
    path + 'Images/Gallery/Italy/Basilica-di-San-Pietro-in-Vaticano2_Lilia-Karakoleva.jpg?width=450&height=450',
    path + 'Images/Gallery/Western-Europe/Austrian-Parliament,-Vienna,-Austria_Gergana-Prokopieva.jpg?width=450&height=450',
    path + 'Images/Gallery/UNESCO-Bulgaria/Ahmed-Bay-Mosque,-Kustendil,-Bulgaria_Svetlin-Nikolaev.jpg?width=450&height=450',
    path + 'Images/Gallery/India/Amber-Fort_Attraction.jpg?width=450&height=450',
    path + 'Images/Gallery/South-Africa/Featherbed-Nature-Reserve_Attraction.jpg?width=450&height=450',
    path + 'Images/Gallery/France/Arc-de-Triomphe_Attraction.jpg?width=450&height=450',
    path + 'Images/Gallery/Costa-Rica/Barra-Honda-National-Park_Daniel-Peichev_Attraction.JPG?width=450&height=450',
    path + 'Images/Gallery/Brazil-and-Argentina/Aconcagua-national-park-Confluencia-camp2_Galabina-Yordanova.JPG?width=450&height=450',
    path + 'Images/Gallery/Cuba/Beach_Kiril-Nedyalkov.jpg?width=450&height=450',
    path + 'Images/Gallery/Egypt/Abu-Simbel-Temples_Dimo-Dimov_Attraction.jpg?width=450&height=450',
    path + 'Images/Gallery/England-Scotland-and-Wales/Bath,-United-Kingdom_Gergana-Prokopieva.JPG?width=450&height=450',
    path + 'Images/Gallery/Myanmar/Mahamuni-Pagoda_Attraction.jpg?width=450&height=450',
    path + 'Images/Gallery/China/Detail-of-bronze-lion-inside-Forbidden-City-in-Beijing,-China.jpg?width=450&height=450',
    path + 'Images/Gallery/Kenya/Giraffe_Herd_in_Field_Kenya_Africa_Ivan-Zhekov.JPG?width=450&height=450',
    path + 'Images/Gallery/New-York-City/Brooklyn-Bridge-View_Pavlina-Hadjieva.JPG?width=450&height=450',
    path + 'Images/Gallery/Turkey-and-Greece/Blue-Mosque,-Istanbul,-Turkey2_Valentin-Likyov.jpg?width=450&height=450',
    path + 'Images/Gallery/Japan/Fuji-san3_Ivan-Zhekov.JPG?width=450&height=450',
    path + 'Images/Gallery/Central-Europe/Berlin-Cathedral,-Berlin,-Germany4.jpg?width=450&height=450',
    path + 'Images/Gallery/Peru/Beach-Zorritos,-Peru_Svetlin-Nikolaev.JPG?width=450&height=450',
    path + 'Images/Gallery/Barcelona-and-Tenerife/Arc-de-Triomf,-Barcelona,-Spain_Liliya-Karakoleva.JPG?width=450&height=450',
    path + 'Images/Gallery/United-States/Boston-Old-South-Church_Ivo-Igov.JPG?width=450&height=450',
    path + 'Images/Gallery/Malta/Bibliotheca-National-Library_Marie-Lan-Nguyen.JPG?width=450&height=450',
    path + 'images/app/homepage-image.jpg',
    path + 'Scripts/register-service-worker.js'
];

self.addEventListener('install', function (e) {
    e.waitUntil(
        caches.open(cacheName).then(function (cache) {
            Promise.all(
                cachedFiles.map(function (url) { cache.add(url) })
            );
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
    if (e.request.url.indexOf("kendo") === -1 &&
        e.request.url.indexOf("tripxpert") === -1
    ) {
        return;
    }

    e.respondWith(
        caches.match(e.request.url).then(function (resp) {
            return resp || fetch(e.request.url).then(function (response) {
                var clonedResponse = response.clone();
                
                if (response.redirected) {
                    return new Response(response.body);
                }

                caches.open(cacheName).then(function (cache) {
                    cache.put(e.request.url, clonedResponse);
                });
                return response;
            });
        }).catch(function () {
            return caches.match('/');
        })
    );
});
