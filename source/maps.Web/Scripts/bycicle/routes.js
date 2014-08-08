function Routes() {
    var _this = this;

    this.map = null;
    this.selectedPolylines = null;

    this.init = function ()
    {
        _this.selectedPolylines = [];
        _this.initializeMap();
        
    }

    this.initializeMap = function () {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(48.9117731, 24.717129),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);

        _this.loadRoutes();
    }

    this.loadNewRoutes = function ()
    {
        var url = $("#map").data("url");
        $.ajax({
            type: "GET",
            url: url,
            success: function(data) {
                if (data.result == "ok") {
                    var opacity = 1 / data.data.length / 2;
                    $.each(data.data, function(i, item) {
                        console.log(item);
                        var path = google.maps.geometry.encoding.decodePath(item);

                        var polyline = new google.maps.Polyline({
                            map: _this.map,
                            path: path,
                            strokeColor: "#ff0000",
                            strokeOpacity: opacity,
                            strokeWeight: 3,
                            zIndex: 400
                        });
                    });
                }
            }
        });
    }

    this.loadRoutes = function () {
        $.ajax({
            type: "GET",
            url: "/bicycle/Route/GetMap",
            success: function (data) {
                if (data.result == "ok") {
                    $.each(data.data, function (i, item) {
                        var path = [
                            _this.getPosition(item.Start),
                            _this.getPosition(item.End)];
                        var polyline = new google.maps.Polyline({
                            map: _this.map,
                            path: path,
                            strokeColor: "#0000aa",
                            strokeWeight: item.Quantity
                        });

                        polyline.set("ID", item.ID);
                        google.maps.event.addListener(polyline, 'click', function (h) {

                            var id = polyline.get("ID");
                            _this.getRoutes(id);
                        });
                    });
                }
            }
        })
    }

    this.getPosition = function (str) {
        var position = str.replace('(', '').replace(')', '');

        var lat = position.replace(/\s*\,.*/, ''); // first 123
        var lng = position.replace(/.*,\s*/, ''); // second ,456

        return new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
    }

    this.getRoutes = function (id) {
        $.ajax({
            type: "GET",
            url: "/bicycle/Route/GetBicycleRoutes",
            data: { id: id },
            success: function (data) {
                _this.clearSelectedPolylines();

                if (data.result == "ok") {
                    $.each(data.data, function (i, item) {
                        var path = google.maps.geometry.encoding.decodePath(item);

                        var polyline = new google.maps.Polyline({
                            map: _this.map,
                            path: path,
                            strokeColor: "#ff0000",
                            strokeOpacity: 0.7,
                            strokeWeight: 3,
                            zIndex: 300
                        });

                        _this.selectedPolylines.push(polyline);
                    });
                }
            }
        })
    }


    this.clearSelectedPolylines = function () {
        for (var i = 0; i < _this.selectedPolylines.length; i++) {
            _this.selectedPolylines[i].setMap(null);
        }
        _this.selectedPolylines = [];
    }
}

var routes = null;

$(function () {
    routes = new Routes();
    routes.init();
});