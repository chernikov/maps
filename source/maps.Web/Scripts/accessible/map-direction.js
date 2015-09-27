function MapDirection() {
    var _this = this;

    this.selectedPolylines = null;

    this.init = function ()
    {
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
                            strokeColor: "#ff0000",
                            strokeWeight: 3,
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
}


var mapDirection = null;
$(function () {
    mapDirection = new MapDirection();
});