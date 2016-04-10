function Edit() {
    var _this = this;

    _this.marker = null;


    this.init = function () {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init(function () {
                var lat = parseFloat($("#Lat").val());
                var lng = parseFloat($("#Lng").val());
                var latLng = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
                if (lat == 0 && lng == 0) {
                    mapMain.map.setOptions({ draggableCursor: 'crosshair' });
                    google.maps.event.addListener(mapMain.map, 'click', _this.clickOnMap);
                } else {
                    var latLng = new google.maps.LatLng(lat, lng);
                    _this.addMarker(latLng);
                }
            });
        });
    }

    this.clickOnMap = function (event) {
        if (_this.marker)
        {
            return;
        }
        _this.addMarker(event.latLng);
        mapMain.map.setOptions({ draggableCursor: 'pointer' });
    }
    this.addMarker = function (position) {
        _this.marker = new google.maps.Marker({
            map: mapMain.map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: position,
        });
        $("#Lat").val(position.lat());
        $("#Lng").val(position.lng());

        google.maps.event.addListener( _this.marker, 'drag', function (event) {
            $("#Lat").val(this.position.lat());
            $("#Lng").val(this.position.lng());
        });
    }
}

var edit = null;
$(function () {
    edit = new Edit();
    edit.init();
})