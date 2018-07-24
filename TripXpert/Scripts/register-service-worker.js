if ('serviceWorker' in navigator) {
    var path = location.pathname;
    debugger
    navigator.serviceWorker
        .register(path.replace("Home","") + 'service-worker.js', { scope: path })
        .then(function (reg) {
            console.log('Registered!');
        })
        .catch(function (err) {
            console.log(err);
        });
}