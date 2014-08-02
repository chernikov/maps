function Parkings() {
    var _this = this;

    this.map = null;
    this.infoWindows = [];
    this.init = function ()
    {
        _this.initializeMap();
        _this.loadParkings();
    }

    this.initializeMap = function () {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(48.9117731, 24.717129),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);
    }

    this.loadParkings = function ()
    {
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
                            position: latLng
                        });
                        if (item.Type == 1) {
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
            success: function (data) 
            {
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

var parkings = null;

$(function () {
    parkings = new Parkings();
    parkings.init();
});