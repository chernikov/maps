function MyRoutes() {
    var _this = this;

    this.map = null;
    this.selectedPolylines = null;

    this.init = function () {
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


    this.loadRoutes = function () {
        var url = $("#map").data("url");
        $.ajax({
            type: "GET",
            url: url,
            success: function(data) {
                if (data.result == "ok") {
                    
                    $.each(data.data, function(i, item) {
                        console.log(item);
                        var path = google.maps.geometry.encoding.decodePath(item);

                        var polyline = new google.maps.Polyline({
                            map: _this.map,
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

    
    this.getPosition = function (str)
    {
        var position = str.replace('(', '').replace(')', '');
        var lat = position.replace(/\s*\,.*/, ''); // first 123
        var lng = position.replace(/.*,\s*/, ''); // second ,456

        return new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
    }
}
var myRoutes = null;
$(function () {
    myRoutes = new MyRoutes();
    myRoutes.init();
});