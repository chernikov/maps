function AddressParking() {
    var _this = this;

    this.map = null;
    this.infowindow = null;
    this.geocoder = null;
    this.init = function() {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
    }

    this.initializeMap = function() {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(centerLat, centerLng),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);
        _this.loadParkings();
        _this.geocoder = new google.maps.Geocoder();
    }

    this.loadParkings = function() {
        $.ajax({
            type: "GET",
            url: "/admin/BicycleParking/getAddress",
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

                        if (item.Type == 1) {
                            marker.setIcon("/Content/images/marker_13_2x.png");
                        }
                        if (item.Type == 2) {
                            marker.setIcon("/Content/images/marker_12_2x.png");
                        }

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

var addressParking = null;
        $(function() {
            addressParking = new AddressParking();
            addressParking.init();
        });