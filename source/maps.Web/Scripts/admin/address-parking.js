function AddressParking() {
    var _this = this;

    this.map = null;
    this.infowindow = null;
    this.geocoder = null;
    this.init = function () {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
    }

    this.initializeMap = function () {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(centerLat, centerLng),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);
        _this.loadParkings();
        _this.geocoder = new google.maps.Geocoder();
    }

    this.loadParkings = function () {
        $.ajax({
            type: "GET",
            url: "/admin/BicycleParking/getAddress",
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

                        _this.geocoder.geocode({ 'latLng': latLng }, function (results, status) {
                            if (status == google.maps.GeocoderStatus.OK)
                            {
                                if (results[0]) {
                                    $.ajax({
                                        type: "GET",
                                        url : "/admin/BicycleParking/SaveAddress",
                                        data: {
                                            id : item.Id,
                                            address : results[0].formatted_address
                                        },
                                        success: function(data) {
                                            if (data.result == "ok") {
                                                console.info("OK");
                                            }
                                        }
                                });
                                    console.log(results[0].formatted_address);
                                }
                            } else {
                                console.error("Geocoder failed due to: " + status);
                            }
                        });
                        return;
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