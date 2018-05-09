/*global window */

if ('serviceWorker' in navigator) {
    navigator.serviceWorker
        .register('/service-worker.js', { scope: './' })
        .then(function (reg) {
            console.log('Registered!');
        })
        .catch(function (err) {
            console.log(err);
        });
}


(function (TX, $) {
    var url = "";
    TX.showDialog = function (sender) {
        url = sender.src;
        var dialog = $("#dialog").data("kendoDialog");
        var dataSource = $(dialog.element).find("[data-role='listview']").data("kendoListView").dataSource;
        dataSource.fetch();
        dialog.open();
    };

    TX.listViewDataBound = function () {
        var dialog = $("#dialog").data("kendoDialog");
        if (this.dataSource.data().length !== 0) {
            dialog.center();
            dialog.title(this.dataSource.data()[0].AttractionTitle);
        }
    };

    TX.imageInfo = function () {
        return { imageURL: url };
    };

    TX.contact_showDialog = function () {
        $("#dialog").data("kendoDialog").open();
    };
})(window.TX = window.TX || {}, $);