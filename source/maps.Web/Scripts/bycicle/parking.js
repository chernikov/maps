function Parking() {
    var _this = this;

    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
            mapParking.init();
        });
    }
}
var parking = null;
$(function () {
    parking = new Parking();
    parking.init();
});