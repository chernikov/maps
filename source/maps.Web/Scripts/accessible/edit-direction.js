function EditDirection() {
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
                _this.directionsDisplay = new google.maps.DirectionsRenderer({
                    draggable: true
                });

                google.maps.event.addListener(_this.directionsDisplay, 'directions_changed', function () {
                    _this.response = _this.directionsDisplay.getDirections();
                   /* if (_this.draggableFirst == 0) {
                        _this.draggableFirst = 1;
                    } else if (_this.draggableFirst == 1) {
                        _this.draggableFirst = 2;
                        $("#third").removeClass("active").addClass("passed");
                        $("#fourth").addClass("active");
                    }*/
                });

                _this.directionsService = new google.maps.DirectionsService();

                $.ajax({
                    type: "GET",
                    url: "/accessible/Direction/Polyline",
                    data: { id: $("#ID").val() },
                    success: function (data) {
                        if (data.result == "ok") {
                            var path = google.maps.geometry.encoding.decodePath(data.data);
                            var waypoints = JSON.parse(data.waypoints);
                            var start = path[0];
                            var end = path[path.length - 1];
                            waypoints = waypoints.slice(1, waypoints.length - 1);
                            if (waypoints.length > 0) {
                                var geocoder = new google.maps.Geocoder;
                                var i = 0;

                                $.each(waypoints, function (i, item) {
                                    geocoder.geocode({ 'placeId': item.place_id }, function (results, status) {
                                        i++;
                                        if (status === google.maps.GeocoderStatus.OK) {
                                            if (results[0]) {
                                                item.location = results[0].geometry.location;
                                            }
                                        }
                                        if (i == waypoints.length) {
                                            _this.processRoute(start, end, waypoints);
                                        }
                                    });
                                });
                            } else {
                                _this.processRoute(start, end, waypoints);
                            }
                        }
                    }
                });
            });
           
        });

        this.processRoute = function (start, end, waypoints)
        {
            var ways = [];
            $.each(waypoints, function (i, item) {
                ways.push({ location: item.location, stopover: false });
            });
            var request = {
                origin: start,
                destination: end,
                waypoints: ways,
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

            $("#first").removeClass("active").addClass("passed");
            $("#second").removeClass("active").addClass("passed");
            $("#third").removeClass("active").addClass("passed");
            $("#fourth").addClass("active");
            $("#SaveRouteBtnOther").removeClass("disabled").removeAttr("disabled");
        }
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


    this.saveRoute = function () {
        if (_this.response != null) {
            var route = _this.response.routes[0];
            var waypts = _this.response.geocoded_waypoints;
            var distance = 0;
            $.each(_this.response.routes[0].legs, function (i, item) {
                distance += item.distance.value;
            });

            $.ajax({
                url: "/accessible/Direction/Save",
                type: "POST",
                data: {
                    ID : $("#ID").val(),
                    Waypoints: JSON.stringify(waypts),
                    PolyLine: route.overview_polyline,
                    Length: distance / 1000,
                },
                success: function (data) {
                    if (data.result == "ok") {
                        window.location = "/accessible/Direction/My";
                    }
                }
            });
        }
    }

}

var editDirection = null;
$(function () {
    editDirection = new EditDirection();
    editDirection.init();
})