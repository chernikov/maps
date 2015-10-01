function MapObject()
{
    var _this = this;
    this.infoWindows = [];


    this.init = function ()
    {
        $.ajax({
            type: "GET",
            url: "/accessible/Object/GetAll",
            success: function (data) {
                if (data.result == "ok") {
                    $.each(data.data, function (i, item) {
                        var latLng = new google.maps.LatLng(parseFloat(item.Lat), parseFloat(item.Lng));
                        var marker = new google.maps.Marker({
                            map: mapMain.map,
                            position: latLng,
                        });
                       /* if (item.Exist)
                        {
                            if (item.Type == 1) {
                                marker.setIcon("/Content/images/icons_bikeparking-02_22x22.png");
                                marker.setZIndex(10);
                            }
                            if (item.Type == 2) {
                                marker.setIcon("/Content/images/icons_bikeparking-01_22x22.png");
                                marker.setZIndex(100);
                            }
                        } else {
                            marker.setIcon("/Content/images/icons_bikeparking-03_22x22.png");
                            marker.setZIndex(9);
                            marker.setOpacity(0.7);
                        }*/
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
            url: "/accessible/Object/Info",
            data: { id: id },
            success: function (data) {
                _this.clearAllInfo();
                var infowindow = new google.maps.InfoWindow({
                    content: data
                });
                _this.infoWindows.push(infowindow);

                infowindow.open(mapMain.map, marker);
                

            }
        });
    }

    this.clearAllInfo = function () {
        for (var i = 0; i < _this.infoWindows.length; i++) {
            _this.infoWindows[i].setMap(null);
        }
        _this.infoWindows = [];
    }
}
var mapObject = null;
$(function ()
{
    mapObject = new MapObject();
});