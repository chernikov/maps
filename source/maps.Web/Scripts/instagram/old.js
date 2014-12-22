function Old() {
    var _this = this;
    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
            mapPhotos.init();
            
        });
    }
}

var old = null;
$(function () {
    old = new Old();
    old.init();
});