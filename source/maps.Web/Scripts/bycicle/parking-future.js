function ParkingFuture() {
    var _this = this;

    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
            mapFutureParking.init();
        });
    }
}
var parkingFuture = null;
$(function () {
    parkingFuture = new ParkingFuture();
    parkingFuture.init();
});