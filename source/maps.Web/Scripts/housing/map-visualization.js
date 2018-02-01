function MapVisualization() {
    var _this = this;
    this.infoWindows = [];
    this.markers = [];
    this.markerClusterer = null;


    this.filterData = function (startYear, lastYear, existAtStartYear)
    {

        _this.clearAll();
        $.ajax({
            type: "POST",
            dataType : "json",
            url: "/api/housing",
            data: {
                StartYear: parseInt(startYear),
                LastYear: parseInt(lastYear),
                ExistOnStartYear: existAtStartYear == "true"
            },
            success: function (data) {
                _this.outputMarkers(data);
            }
        });
    }

    this.showData = function () {
        _this.clearAll();
        $.ajax({
            type: "POST",
            url: "/api/housing",
            success: function (data) {
                _this.outputMarkers(data);
            }
        });
    }

    this.outputMarkers = function (data)
    {
        $.each(data, function (i, item) {
            if (item.Lat != 0 && item.Lng != 0) {
                var image = null;
                var big = item.Capacity >= 100 ? "_big" : "";
                var size = item.Capacity >= 100 ? new google.maps.Size(24, 24) : new google.maps.Size(16, 16);
                var origin = new google.maps.Point(0, 0);
                var anchor = item.Capacity >= 100 ? new google.maps.Point(-12, -12) : new google.maps.Point(-8, -8);

                var pecent = (item.Capacity - item.OnEnd) / item.Capacity * 100;
                var tenPercent = item.Capacity / 10;
                if (item.Capacity - item.OnEnd > 10 && pecent > 15)
                {
                    image = {
                        url: '/Content/images/icons/circle_yellow' + big + '.png',
                        size: size,
                        origin: origin,
                        anchor: anchor
                    };
                } else {
                    if (item.OnStart)
                    {
                        if (Math.abs(item.OnStart - item.OnEnd) < tenPercent) {
                            image = {
                                url: '/Content/images/icons/circle_grey' + big + '.png',
                                size: size,
                                origin: origin,
                                anchor: anchor
                            };
                        } else if (item.OnStart < item.OnEnd) {
                            image = {
                                url: '/Content/images/icons/circle_green' + big + '.png',
                                size: size,
                                origin: origin,
                                anchor: anchor
                            };
                        } else if (item.OnStart > item.OnEnd) {
                            image = {
                                url: '/Content/images/icons/circle_red' + big + '.png',
                                size: size,
                                origin: origin,
                                anchor: anchor
                            };
                        }
                    } else {
                        image = {
                            url: '/Content/images/icons/circle_green' + big + '.png',
                            size: size,
                            origin: origin,
                            anchor: anchor
                        };
                    }
                }
                    
                var latLng = new google.maps.LatLng(parseFloat(item.Lat), parseFloat(item.Lng));
                var marker = new google.maps.Marker({
                    map: mapMain.map,
                    position: latLng,
                    icon: image
                });
                google.maps.event.addListener(marker, 'click', function () {
                    _this.showInfo(marker, item);
                });
                _this.markers.push(marker);
            } 
        });
    }

    this.clearAll = function () {
        for (var i = 0; i < _this.markers.length; i++) {
            _this.markers[i].setMap(null);
        }
        _this.markers = [];

        if (_this.markerClusterer != null) {
            _this.markerClusterer.clearMarkers();
        }
    }

    this.showInfo = function (marker, item)
    {
        _this.clearAllInfo();
        var infowindow = new google.maps.InfoWindow({
            content: "<ul><li><strong>Адреса:</strong>"+item.Address+"</li>"+
                    "<li><strong>Кількість:</strong>"+item.Capacity+"</li>"+
                    "<li><strong>Початок:</strong>" + item.OnStart + "</li>" +
                    "<li><strong>Кінець:</strong>"+item.OnEnd+"</li></ul>"
        });
        _this.infoWindows.push(infowindow);
        infowindow.open(mapMain.map, marker);

        _this.showDetails(item.Id);
    }

    this.clearAllInfo = function () {
        for (var i = 0; i < _this.infoWindows.length; i++) {
            _this.infoWindows[i].setMap(null);
        }
        _this.infoWindows = [];
    }

    this.showDetails  = function(id) 
    {
        $.ajax({
            type: "GET",
            url: "/housing/Home/Data",
            data:  {
                id : id
            },
            success: function (data) {
                $("#DataWrapper").html(data);
            }
        })
    }
}

var mapVisualization = null;
$(function () {
    mapVisualization = new MapVisualization();
});