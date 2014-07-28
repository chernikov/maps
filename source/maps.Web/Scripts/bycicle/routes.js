function Routes() {
    var _this = this;

    this.map = null;

    this.init = function ()
    {
        _this.initializeMap();
        _this.loadRoutes();
    }

    this.initializeMap = function () {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(48.9117731, 24.717129),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);
    }

    this.loadRoutes = function ()
    {
        var url = $("#map").data("url");
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                if (data.result == "ok")
                {
                    var opacity = 1 / data.data.length * 10;
                    $.each(data.data, function (i, item)
                    {
                        console.log(item);
                        var path = google.maps.geometry.encoding.decodePath(item);

                        var polyline = new google.maps.Polyline({
                            map: _this.map,
                            path: path,
                            strokeColor: "#ff0000",
                            strokeOpacity: opacity,
                            strokeWeight : 5
                        });
                    });
                }
            }
        })
    }
}

var routes = null;

$(function () {
    routes = new Routes();
    routes.init();
});