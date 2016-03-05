function MapVisualization()
{
    var _this = this;
    this.infoWindows = [];
    this.markers = [];
    this.markerClusterer = null;

    this.showData = function (id)
    {
        _this.clearAll();
        $.ajax({
            type: "GET",
            url: "/vision-zero/Home/ShowData",
            data : {
                id : id
            },
            success: function (data) {
                _this.outputMarkers(data);
            }
        });
    }

    this.filterData = function (id, filter) {
        _this.clearAll();
        $.ajax({
            type: "GET",
            url: "/vision-zero/Home/FilterData",
            data: filter,
            success: function (data) {
                _this.outputMarkers(data);
            }
        });
    }

    this.outputMarkers = function (data) {
        if (data.result == "ok") {
            $.each(data.data, function (i, item) {
                var latLng = new google.maps.LatLng(parseFloat(item.lat), parseFloat(item.lng));
                var marker = new google.maps.Marker({
                    /*     map: mapMain.map,      */
                    position: latLng
                });
                marker.set("Id", item.Id);
                google.maps.event.addListener(marker, 'click', function () {
                    _this.showInfo(marker);
                });
                _this.markers.push(marker);
            });


            _this.markerClusterer = new MarkerClusterer(mapMain.map, _this.markers,
            {
                maxZoom: 18,
                gridSize: 10,
                styles: null
            });
            $("#CountWrapper").text(_this.markers.length + " items");
        }
    }

    this.clearAll = function ()
    {
        for (var i = 0; i < _this.markers.length; i++) {
            _this.markers[i].setMap(null);
        }
        _this.markers = [];          

        if (_this.markerClusterer != null) {
            _this.markerClusterer.clearMarkers();
        }
    }

    this.showInfo = function (marker) {
        var id = marker.get("Id");
        $.ajax({
            type: "GET",
            url: "/vision-zero/Home/Info",
            data: { id: id },
            success: function (data) {
                _this.clearAllInfo();
                var infowindow = new google.maps.InfoWindow({
                    content: data
                });
                _this.infoWindows.push(infowindow);
                infowindow.open(mapMain.map, marker);
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


var mapVisualization = null;
$(function ()
{
    mapVisualization = new MapVisualization();
});