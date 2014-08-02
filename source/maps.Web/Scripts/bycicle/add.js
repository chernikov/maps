function Add() {
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
        $(document).on("click", "#CancelBtn", function () {
            _this.clear();
        });
        $("#ActionWrapper").hide();
    }

    this.initializeMap = function () {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(48.9117731, 24.717129),
            draggableCursor: 'crosshair'
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);
        //_this.map.setOptions({  });

        google.maps.event.addListener(_this.map, 'click', _this.clickOnMap);

        _this.directionsDisplay = new google.maps.DirectionsRenderer({
            draggable : true
        });

        google.maps.event.addListener(_this.directionsDisplay, 'directions_changed', function () {
            _this.response = _this.directionsDisplay.getDirections();
        });
    }

    this.clickOnMap = function (event) {
        if (_this.markers.length < 2)
        {
            _this.addMarker(event.latLng);
        } 
    }

    this.addMarker = function (position)
    {
        var marker = new google.maps.Marker({
            map: _this.map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: position,
        });
        //при окончании перемещения маркера установить функцию 
        google.maps.event.addListener(marker, 'dragend', _this.markerPositionChanged);
        _this.markers.push(marker);
        _this.createRoute();
    }

    //получить координаты и информацию о местоположении
    this.markerPositionChanged = function () {
        _this.createRoute();
    }


    this.createRoute = function () {
        if (_this.markers.length > 1) {
            var start = _this.markers[0].position;
            var end = _this.markers[_this.markers.length-1].position;

            var request = {
                origin: start,
                destination: end,
                waypoints: [],
                optimizeWaypoints: false,
                travelMode: google.maps.TravelMode.WALKING
            };

            _this.directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    _this.response = response;
                    _this.directionsDisplay.setMap(_this.map);
                    _this.directionsDisplay.setDirections(response);
                }
            });

            _this.clearMarkers();
            _this.map.setOptions({ draggableCursor: ""});
            $("#ActionWrapper").show();
        }
    }

    this.saveRoute = function () {
        if (_this.response != null)
        {
            var route = _this.response.routes[0];
            var waypts = _this.response.routes[0].overview_path;

            $.ajax({
                url : "/bicycle/Route/SaveRoute",
                type: "POST",
                data: {
                    Waypoints: JSON.stringify(waypts),
                    PolyLine : route.overview_polyline,
                },
                success: function (data) {
                    if (data.result == "ok")
                    {
                        _this.clear();
                        window.location = "/bicycle/Route";
                    }
                }
            });
        }
    }


    this.clearMarkers = function ()
    {
        for (var i = 0; i < _this.markers.length; i++) {
            _this.markers[i].setMap(null);
        }
        _this.markers = [];
    }

    this.clear = function () {
        _this.clearMarkers();
        _this.directionsDisplay.setMap(null);
        _this.map.setOptions({ draggableCursor: "crosshair" });
        $("#ActionWrapper").hide();
    }
}

var add = null;

$(function () {
    add = new Add();
    add.init();
})