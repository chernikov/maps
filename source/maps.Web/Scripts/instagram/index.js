﻿function Index() {
    var _this = this;
    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
            mapPhotos.init();
            
        });
    }
}

var index = null;
$(function () {
    index = new Index();
    index.init();
});