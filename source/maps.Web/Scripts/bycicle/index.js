﻿function Index() {
    var _this = this;
    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
            mapParking.init();
            mapRoute.init();
        });
    }
}

var index = null;
$(function () {
    index = new Index();
    index.init();
});