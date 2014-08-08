function RouteLengths() {
    var _this = this;

    this.map = null;
    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
    }

    this.initializeMap = function () {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(centerLat, centerLng),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);

        _this.loadNewRoutes();
    }

    this.loadNewRoutes = function () {
        var url = $("#map").data("url");
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                if (data.result == "ok") {
                    $.each(data.data, function (i, item) {
                        var path = google.maps.geometry.encoding.decodePath(item.PolyLine);
                        var polyline = new google.maps.Polyline({
                            map: _this.map,
                            path: path,
                            strokeColor: "#ff0000",
                            strokeWeight: 3,
                            zIndex: 400
                        });
                        var length = polyline.Distance();

                        $.ajax({
                            type: "GET",
                            async: false,
                            url: "/admin/BicycleRoute/SaveLength",
                            data: {
                                id: item.ID,
                                length: length,
                            },
                            success: function (data)
                            {
                                if (data.result == "ok")
                                {
                                    console.log("OK: " + item.ID);
                                }
                            }
                        });
                        console.log();
                    });
                }
            }
        });
    }

}

var routeLengths = null;

$(function() {
    routeLengths = new RouteLengths();
    routeLengths.init();
});