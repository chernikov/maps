function MapPhotos()
{
    var _this = this;

    this.markers = [];
    this.infoWindows = [];
    this.init = function ()
    {
        $.ajax({
            type: "GET",
            url: "/instagram/Home/List",
            success: function (data) {
                if (data.result = "ok") {
                    $.each(data.data, function (index, item) {
                        _this.createPhoto(item);
                    });
                }
            }
        });
    }

    this.createPhoto = function (item)
    {
        var latLng = new google.maps.LatLng(parseFloat(item.Lat), parseFloat(item.Lng));
        var marker = new google.maps.Marker({
            map: mapMain.map,
            position: latLng
        });
        marker.set("Id", item.Id);
        marker.set("PhotoUrl", item.PhotoUrl);
        google.maps.event.addListener(marker, 'click', function () {
            _this.showInfo(marker);
        });
        _this.markers.push(marker);
    }


    this.showInfo = function (marker) {
        var id = marker.get("Id");
        var photoUrl = marker.get("PhotoUrl");
        _this.clearAllInfo();
        var data = "<div class=\"instagram-photo\"><img src=\"" + photoUrl + "\" /></div>";
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


var mapPhotos = null;
$(function () {
    mapPhotos = new MapPhotos();
});