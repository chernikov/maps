function MapSmallMain()
{
    var _this = this;

    this.map = null;
    this.centerLat = null;
    this.centerLng = null;
    this.zoom = null;
    this.init = function (callback)
    {
        _this.centerLat = $("#map").data("lat");
        _this.centerLng = $("#map").data("lng");
        _this.zoom = $("#map").data("zoom");

        var mapOptions = {
            zoom: _this.zoom,
            center: new google.maps.LatLng(_this.centerLat, _this.centerLng),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);

        if (callback != null) {
            callback();
        }
    }
}


var mapSmallMain = null;
$(function () {
    mapSmallMain = new MapSmallMain();
});