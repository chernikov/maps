function MapPhoto()
{
    var _this = this;

    this.markers = [];
    this.infoWindows = [];
    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();

            _this.createPhoto();
        });
    }

    this.createPhoto = function ()
    {
        var lat = $("#Lat").val();
        var lng = $("#Lng").val();
        var photoUrl = $("#PhotoUrl").val();
        var id = $("#ID").val();
        var latLng = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));

        var marker = new google.maps.Marker({
            map: mapMain.map,
            position: latLng
        });
        marker.set("Id", id);
        marker.set("PhotoUrl", photoUrl);
        google.maps.event.addListener(marker, 'click', function () {
            _this.showInfo(marker);
        });
        _this.markers.push(marker);
    }


    this.showInfo = function (marker) {
        var id = marker.get("Id");
        var photoUrl = marker.get("PhotoUrl");
        _this.clearAllInfo();
        var data = "<div><img src=\"" + photoUrl + "\" /></div>";
        var infoWindow = new google.maps.InfoWindow({
            content: data
        });
        infoWindow.open(mapMain.map, marker);
        _this.infoWindows.push(infoWindow);
    }


    this.clearAllInfo = function () {
        for (var i = 0; i < _this.infoWindows.length; i++) {
            _this.infoWindows[i].setMap(null);
        };
        _this.infoWindows = [];
    }
}


var mapPhoto = null;
$(function () {
    mapPhoto = new MapPhoto();
    mapPhoto.init();
});