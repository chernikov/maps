function AddDirection() {
    var _this = this;
    

    this.directionsDisplay = null;
    this.markers = [];
    this.directionsService = null;
    this.response = null;
    this.draggableFirst = false;

    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {

            mapMain.init(function ()
            {
                mapMain.map.setOptions({ draggableCursor: 'crosshair' });
                google.maps.event.addListener(mapMain.map, 'click', _this.clickOnMap);

                _this.directionsDisplay = new google.maps.DirectionsRenderer({
                    draggable: true
                });

                google.maps.event.addListener(_this.directionsDisplay, 'directions_changed', function () {
                    _this.response = _this.directionsDisplay.getDirections();
                    if (_this.draggableFirst == 0) {
                        _this.draggableFirst = 1;
                    } else if (_this.draggableFirst == 1) {
                        _this.draggableFirst = 2;
                        $("#third").removeClass("active").addClass("passed");
                        $("#fourth").addClass("active");
                    }
                });
            });

            _this.directionsService = new google.maps.DirectionsService();
        });

        $(document).on("click", "#SaveRouteBtn, #SaveRouteBtnOther", function ()
        {
            $(".tutorial-wrapper div").css("text-decoration", "line-through").removeClass("active");
            $("#SaveRouteBtnOther").addClass("disabled").attr("disabled");
            _this.saveRoute();
        });

        $(document).on("click", "#CancelBtn", function () {
            _this.clear();
            $(".tutorial-wrapper div").css("text-decoration", "");
            $(".tutorial-item").removeClass("passed active");
            $("#first").addClass("active");
            $("#SaveRouteBtnOther").addClass("disabled").attr("disabled");
            location.reload(); //temporary fix
        });
        $("#ActionWrapper").hide();
        $("#first").addClass("active");
    }


    this.clickOnMap = function (event) {
        var markersLength = _this.markers.length;
        if (markersLength === 0) {
            $("#first").removeClass("active").addClass("passed");
            if (!$("#second").hasClass("passed"))
            {
                $("#second").addClass("active");
            }
        }
        if (markersLength === 1)
        {
            $("#second").removeClass("active").addClass("passed");
            $("#third").addClass("active");
            $("#SaveRouteBtnOther").removeClass("disabled").removeAttr("disabled");
        }
        if (markersLength < 2 && _this.response == null) {
            _this.addMarker(event.latLng);
        }
    }

    this.addMarker = function (position) {
        var marker = new google.maps.Marker({
            map: mapMain.map,
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
            var end = _this.markers[_this.markers.length - 1].position;
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
                    _this.directionsDisplay.setMap(mapMain.map);
                    _this.directionsDisplay.setDirections(response);
                }
            });

            _this.clearMarkers();
            mapMain.map.setOptions({ draggableCursor: "" });
            $("#ActionWrapper").show();
        }
    }

    this.saveRoute = function () {
        if (_this.response != null) {
            var route = _this.response.routes[0];
            var waypts = _this.response.routes[0].overview_path;
            var distance = 0;
            $.each(_this.response.routes[0].legs, function (i, item) {
                distance += item.distance.value;
            });

            $.ajax({
                url: "/accessible/Direction/Save",
                type: "POST",
                data: {
                    Waypoints: JSON.stringify(waypts),
                    PolyLine: route.overview_polyline,
                    Length: distance / 1000,
                },
                success: function (data) {
                    if (data.result == "ok") {
                        _this.clear();
                        window.location = "/accessible/Direction";
                    }
                }
            });
        }
    }

    this.clearMarkers = function () {
        for (var i = 0; i < _this.markers.length; i++) {
            _this.markers[i].setMap(null);
        }
        _this.markers = [];
    }

    this.clear = function () {
        _this.clearMarkers();
        _this.directionsDisplay.setMap(null);
        mapMain.map.setOptions({ draggableCursor: "crosshair" });
        $("#ActionWrapper").hide();
    }
}

var addDirection = null;
$(function () {
    addDirection = new AddDirection();
    addDirection.init();
})