if ('serviceWorker' in navigator) {
    var path = location.pathname;

    navigator.serviceWorker
        .register(path + 'service-worker.js', { scope: path })
        .then(function (reg) {
            console.log('Registered!');
        })
        .catch(function (err) {
            console.log(err);
        });
}