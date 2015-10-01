function AccessiblePlace() {
    var _this = this;

    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
            mapPlace.init();
        });
    }
}
var accessiblePlace = null;
$(function () {
    accessiblePlace = new AccessiblePlace();
    accessiblePlace.init();
});