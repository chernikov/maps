function BicycleEditParking() {
    var _this = this;

    this.map = null;
    this.marker = null;
    this.centerLat = null;
    this.centerLng = null;
    this.zoom = null;

    this.geocoder = null;
    this.init = function () 
    {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
        _this.initPhoto()
    }

    this.initializeMap = function () 
    {
        _this.centerLat = $("#map").data("lat");
        _this.centerLng = $("#map").data("lng");
        _this.zoom = $("#map").data("zoom");
        _this.geocoder = new google.maps.Geocoder();

        var latLng = new google.maps.LatLng(_this.centerLat, _this.centerLng);
        if ($("#Position").val() != "") 
        {
            var position = $("#Position").val().replace('(', '').replace(')', '');
            var lat = position.replace(/\s*\,.*/, ''); // first 123
            var lng = position.replace(/.*,\s*/, ''); // second ,456
            latLng = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
        };
        var mapOptions = {
            zoom: _this.zoom,
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

    }

    this.markerPositionChanged = function ()
    {
        $("#Position").val(_this.marker.position.toString());

        var center = new google.maps.LatLng(_this.centerLat, _this.centerLng);
        var distance = _this.marker.position.distanceFrom(center);
        $("#CenterDistance").val(distance);
        //Address
        _this.geocoder.geocode({ 'latLng': _this.marker.position }, function (results, status)
        {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    $("#Address").val(results[0].formatted_address);
                }
            } else {
                console.error("Geocoder failed due to: " + status);
            }
        });
    }

    this.initPhoto = function ()
    {
        $("#ChangePhoto").fineUploader({
            element: $('#ChangePhoto'),
            request: {
                endpoint: "/File/UploadFile"
            },
            template: 'qq-template-bootstrap',
            allowedExtensions: ['jpg', 'jpeg', 'png', 'gif'],
            classes: {
                success: 'alert alert-success',
                fail: 'alert alert-error'
            },
            failedUploadTextDisplay: {
                mode: 'custom',
                maxChars: 400,
                responseProperty: 'error',
                enableTooltip: true
            },
            debug: true
        })
      .on('complete', function (event, id, filename, responseJSON) {
          if (responseJSON.success) {
              $("#PhotoUrl").val(responseJSON.fileUrl);
              $("#ParkingImage").attr("src", responseJSON.fileUrl + "?w=300&h=300&mode=crop");
          }
      }).on('submit', function () {
          $(".qq-upload-fail").remove();
          return true;
      });
    }
}

var bicycleEditParking = null;
$(function ()
{
    bicycleEditParking = new BicycleEditParking();
    bicycleEditParking.init();
})