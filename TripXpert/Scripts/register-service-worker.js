if ('serviceWorker' in navigator) {
    navigator.serviceWorker
        .register('./service-worker.js', { scope: './' })
        .then(function (reg) {
            console.log('Registered!');
        })
        .catch(function (err) {
            console.log(err);
        });
}