var cacheName = 'v1';

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
    e.respondWith(
        caches.match(e.request.url).then(function (resp) {
            return resp || fetch(e.request.url).then(function (response) {
                var clonedResponse = response.clone();
                
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
