﻿function Index() {
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
        
        google.maps.event.addListener(_this.map, 'click', _this.clickOnMap);
        _this.directionsDisplay = new google.maps.DirectionsRenderer({
            draggable : true
        });
    }

    this.clickOnMap = function (event) {
        if (_this.markers.length < 2)
        {
            _this.addMarker(event.latLng);
        } 
    }

    this.addMarker = function (position) {
        
        var marker = new google.maps.Marker({
            map: _this.map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: position,
        });
        //при окончании перемещения маркера установить функцию 
        google.maps.event.addListener(marker, 'dragend', _this.markerPositionChanged);

        _this.markers.push(marker);
        _this.changeRoute();
    }

    //получить координаты и информацию о местоположении
    this.markerPositionChanged = function () {
        _this.changeRoute();
    }


    this.changeRoute = function () {
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
        }
    }

    this.saveRoute = function () {
        if (_this.response != null)
        {
            var route = _this.response.routes[0];
            var waypts = [];
            $.each(_this.markers, function (i, item) {
                waypts.push(item.position);
            });
            
            $.ajax({
                url : "bycicle/Home/SaveRoute",
                type: "POST",
                data: {
                    Waypoints: JSON.stringify(waypts),
                    PolyLine : route.overview_polyline,
                },
                success: function (data) {
                    if (data.result == "ok") {
                        for (var i = 0; i < _this.markers.length; i++) {
                            _this.markers[i].setMap(null);
                        }
                        _this.directionsDisplay.setMap(null);
                        _this.markers = [];
                    }
                }
            });
        }
    }
}

var index = null;

$(function () {
    index = new Index();
    index.init();
})