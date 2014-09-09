function DistanceParking() {
    var _this = this;

    this.map = null;
    this.infowindow = null;
    this.geocoder = null;

    this.centerLat = null;
    this.centerLng = null;
    this.zoom = null;

    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
    }

    this.initializeMap = function ()
    {
        _this.centerLat = $("#map").data("lat");
        _this.centerLng = $("#map").data("lng");
        _this.zoom = $("#map").data("zoom");

        var mapOptions = {
            zoom: _this.zoom,
            center: new google.maps.LatLng(_this.centerLat, _this.centerLng),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);
        _this.loadParkings();
        _this.geocoder = new google.maps.Geocoder();
    }

    this.loadParkings = function() {
        $.ajax({
            type: "GET",
            url: "/admin/BicycleParking/List",
            success: function(data) {
                if (data.result == "ok") {
                    $.each(data.data, function(i, item) {
                        console.log(item.Position);
                        var position = item.Position.replace('(', '').replace(')', '');

                        var lat = position.replace(/\s*\,.*/, ''); // first 123
                        var lng = position.replace(/.*,\s*/, ''); // second ,456
                        var latLng = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
                        var marker = new google.maps.Marker({
                            map: _this.map,
                            position: latLng
                        });

                        if (item.Exist) {
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
                        var centerLat = item.CityCenterLat;
                        var centerLng = item.CityCenterLng;
                        var center = new google.maps.LatLng(centerLat, centerLng);

                        var distance = latLng.distanceFrom(center);
                        $.ajax({
                            type: "GET",
                            url: "/admin/BicycleParking/SaveDistance",
                            data: {
                                id: item.Id,
                                distance: distance
                            },
                            success: function(data) {
                                if (data.result == "ok") {
                                    console.info("OK");
                                }
                            }
                        });
                    });
                }
            }
        });
    }
}

var distanceParking = null;
$(function() {
    distanceParking = new DistanceParking();
    distanceParking.init();
});