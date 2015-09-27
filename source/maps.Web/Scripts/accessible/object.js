function AccessibleObject() {
    var _this = this;

    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
            mapParking.init();
        });
    }
}
var accessibleObject = null;
$(function () {
    accessibleObject = new AccessibleObject();
    accessibleObject.init();
});