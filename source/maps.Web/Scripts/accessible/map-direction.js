function MapDirection() {
    var _this = this;

    this.selectedPolylines = null;

    this.infowindow = null;
    this.init = function () {
        _this.selectedPolylines = [];
        $.ajax({
            type: "GET",
            url: "/accessible/Direction/GetMap",
            success: function (data) {
                if (data.result == "ok") {
                    $.each(data.data, function (i, item) {
                        console.log(item);
                        var path = google.maps.geometry.encoding.decodePath(item);
                        var polyline = new google.maps.Polyline({
                            map: mapMain.map,
                            path: path,
                            strokeColor: "#00AE98",
                            strokeWeight: 5,
                            zIndex: 400
                        });
                    });
                }
            }
        });
    }


    this.loadMyRoutes = function () {
        $.ajax({
            type: "GET",
            url: "/accessible/Direction/GetMine",
            success: function (data) {
                if (data.result == "ok") {

                    $.each(data.data, function (i, item) {
                        console.log(item);
                        var path = google.maps.geometry.encoding.decodePath(item.Polyline);

                        var polyline = new google.maps.Polyline({
                            map: mapMain.map,
                            path: path,
                            strokeColor: "#00AE98",
                            strokeWeight: 5,
                            zIndex: 400
                        });
                        polyline.id = item.Id;
                        google.maps.event.addListener(polyline, 'click', function (e) {

                            $.ajax({
                                type: "GET",
                                url: "/accessible/Direction/SelectAction",
                                data: { id: polyline.id },
                                success: function (data) {
                                    _this.infowindow = new google.maps.InfoWindow({
                                        content: data
                                    });
                                    _this.infowindow.setPosition(e.latLng);
                                    _this.infowindow.open(mapMain.map, polyline);
                                }
                            })

                        });
                    });
                }
            }
        });


        $(document).on("click", ".editAccessibleDirection", function (e) {
            if (_this.infowindow)
            {
                _this.infowindow.close();
                _this.infowindow.setMap(null);
            }
            window.location = "/accessible/Direction/Edit/" + $(this).data("id");
        });

        $(document).on("click", ".removeAccessibleDirection", function (e) {
            if (_this.infowindow) {
                _this.infowindow.close();
                _this.infowindow.setMap(null);
            }
            if (confirm("Дійсно видалити?")) {
                window.location = "/accessible/Direction/Remove/" + $(this).data("id");
            };
        });
    }
}


var mapDirection = null;
$(function () {
    mapDirection = new MapDirection();
});