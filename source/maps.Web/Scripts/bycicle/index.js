function Index() {
    var _this = this;

    this.map = null;
    this.selectedPolylines = null;
    this.infoWindows = [];

    this.init = function () {
        _this.selectedPolylines = [];
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
    }

    this.initializeMap = function () {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(centerLat, centerLng),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);

        _this.loadRoutes();
        _this.loadParkings();
    }

    this.loadRoutes = function () {
        var url = $("#map").data("url");
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

    this.loadParkings = function () {
        $.ajax({
            type: "GET",
            url: "/bicycle/parking/getAll",
            success: function (data) {
                if (data.result == "ok") {
                    $.each(data.data, function (i, item) {
                        console.log(item.Position);
                        var position = item.Position.replace('(', '').replace(')', '');

                        var lat = position.replace(/\s*\,.*/, ''); // first 123
                        var lng = position.replace(/.*,\s*/, ''); // second ,456
                        var latLng = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
                        var marker = new google.maps.Marker({
                            map: _this.map,
                            position: latLng});

                        if (item.Type == 1)
                        {
                            marker.setIcon("/Content/images/marker_13_2x.png");
                        }
                        if (item.Type == 2) {
                            marker.setIcon("/Content/images/marker_12_2x.png");
                        }
                        marker.set("Id", item.Id);
                        google.maps.event.addListener(marker, 'click', function () {
                            var id = marker.get("Id");
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
            url: "/bicycle/Parking/Info",
            data: { id: id },
            success: function (data) {
                _this.clearAllInfo();
                var infowindow = new google.maps.InfoWindow({
                    content: data
                });
                infowindow.open(_this.map, marker);
                _this.infoWindows.push(infowindow);

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

var index = null;

$(function () {
    index = new Index();
    index.init();
})