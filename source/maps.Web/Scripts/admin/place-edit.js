function PlaceEdit() {
    var _this = this;

    this.map = null;
    this.marker = null;
    this.init = function () 
    {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
    }

    this.initializeMap = function () 
    {
        var latLng = new google.maps.LatLng(49.0275, 31.482778);
        var zoom = 9;

        if ($("#Lat").val() != "" && $("#Lng").val() != "")
        {
            latLng = new google.maps.LatLng(parseFloat($("#Lat").val()), parseFloat($("#Lng").val()));
        }
        if ($("#Zoom").val() != 0) {
            zoom = parseInt($("#Zoom").val());
        }
        var mapOptions = {
            zoom: zoom,
            center: latLng,
            disableDefaultUI: true,
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);
        _this.marker = new google.maps.Marker({
            map: _this.map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: latLng,
        });
        //при окончании перемещения маркера установить функцию 
        google.maps.event.addListener(_this.marker, 'dragend', _this.markerPositionChanged);

        google.maps.event.addDomListener(_this.map, 'zoom_changed', _this.changeZoom);
    }

    this.markerPositionChanged = function ()
    {
        var position = _this.marker.position;
        $("#Lat").val(position.lat());
        $("#Lng").val(position.lng());
    }

    this.changeZoom = function ()
    {
        var zoom = _this.map.getZoom();
        $("#Zoom").val(zoom);
    }

}

var placeEdit = null;
$(function ()
{
    placeEdit = new PlaceEdit();
    placeEdit.init();
})