function Index() {
    var _this = this;

    this.map = null;
    this.directionsDisplay = null;
    this.markers = [];
    this.directionsService = null;
    this.response = null;
    this.init = function () {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
        _this.directionsService = new google.maps.DirectionsService();

        $(document).on("click", "#SaveRouteBtn", function () {
            _this.saveRoute();
        });
    }

    this.initializeMap = function () {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(48.9117731, 24.717129),
            disableDefaultUI: true
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);
    }
}

var index = null;

$(function () {
    index = new Index();
    index.init();
})