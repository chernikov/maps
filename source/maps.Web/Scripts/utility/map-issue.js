function MapIssue()
{
    var _this = this;
    this.infoWindows = [];

    this.init = function () {
        $.ajax({
            type: "GET",
            url: "/utility/Home/GetAll",
            success: function (data) {
                if (data.result == "ok") {
                    $.each(data.data, function (i, item) {
                        
                        var latLng = new google.maps.LatLng(parseFloat(item.Lat), parseFloat(item.Lng));
                        var marker = new google.maps.Marker({
                            map: mapMain.map,
                            position: latLng,
                            icon: "/Content/images/" + item.Status,
                            anchor: new google.maps.Point(-10, -10)
                        });
                        marker.set("Id", item.Id);
                        google.maps.event.addListener(marker, 'click', function () {
                            _this.showInfo(marker);
                        });
                    });
                }
            }
        });
    }

    this.showInfo = function (marker) {
        var id = marker.get("Id");
        $.ajax({
            type: "GET",
            url: "/utility/Home/Info",
            data: { id: id },
            success: function (data) {
                _this.clearAllInfo();
                var infoWindow = new google.maps.InfoWindow({
                    content: data
                });
                infoWindow.open(mapMain.map, marker);
                _this.infoWindows.push(infoWindow);

            }
        });
    }

    this.clearAllInfo = function () {
        for (var i = 0; i < _this.infoWindows.length; i++)
        {
            _this.infoWindows[i].setMap(null);
        }
        _this.infoWindows = [];
    }
}


var mapIssue = null;
$(function () {
    mapIssue = new MapIssue();
});
