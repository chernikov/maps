﻿function MapFutureParking()
{
    var _this = this;
    this.infoWindows = [];


    this.init = function ()
    {
        $.ajax({
            type: "GET",
            url: "/bicycle/parking/getFuture",
            success: function (data) {
                if (data.result == "ok") {
                    $.each(data.data, function (i, item) {
                        var position = item.Position.replace('(', '').replace(')', '');

                        var lat = position.replace(/\s*\,.*/, ''); 
                        var lng = position.replace(/.*,\s*/, ''); 

                        var latLng = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
                        var marker = new google.maps.Marker({
                            map: mapMain.map,
                            position: latLng
                        });
                        if (item.Exist)
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
                        }
                        marker.set("Id", item.Id);
                        google.maps.event.addListener(marker, 'click', function () {
                            _this.showInfo(marker);
                        });
                    });
                }
            }
        });

        $(document).on("click", ".vote", function () {
            var id = $(this).data("id");
            $.ajax({
                type: "GET",
                url: "/bicycle/Parking/SaveVote",
                data: { id: id },
                success: function (data) {
                    if (data.result == "ok") {
                        _this.clearAllInfo();
                    }
                }
            });
        });
    }


    this.showInfo = function (marker) {
        var id = marker.get("Id");
        $.ajax({
            type: "GET",
            url: "/bicycle/Parking/Info",
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


var mapFutureParking = null;
$(function ()
{
    mapFutureParking = new MapFutureParking();
});