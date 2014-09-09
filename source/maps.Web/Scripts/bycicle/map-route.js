function MapRoute() {
    var _this = this;

    this.selectedPolylines = null;

    this.init = function ()
    {
        _this.selectedPolylines = [];
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
                            map: mapMain.map,
                            path: path,
                            strokeColor: "#0000aa",
                            strokeWeight: item.Quantity
                        });

                        polyline.set("ID", item.ID);
                        google.maps.event.addListener(polyline, 'click', function (h)
                        {
                            var id = polyline.get("ID");
                            _this.getRoutes(id);
                        });
                    });
                }
            }
        });
    }


    this.loadMyRoutes = function () {
        $.ajax({
            type: "GET",
            url: "/bicycle/Route/GetMine",
            success: function (data) {
                if (data.result == "ok") {

                    $.each(data.data, function (i, item) {
                        console.log(item);
                        var path = google.maps.geometry.encoding.decodePath(item);

                        var polyline = new google.maps.Polyline({
                            map: mapMain.map,
                            path: path,
                            strokeColor: "#ff0000",
                            strokeWeight: 3,
                            zIndex: 400
                        });
                    });
                }
            }
        });
    }


    this.getPosition = function (str) {
        var position = str.replace('(', '').replace(')', '');

        var lat = position.replace(/\s*\,.*/, ''); 
        var lng = position.replace(/.*,\s*/, '');

        return new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
    }

    this.getRoutes = function (id)
    {
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
                            map: mapMain.map,
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


var mapRoute = null;
$(function () {
    mapRoute = new MapRoute();
});