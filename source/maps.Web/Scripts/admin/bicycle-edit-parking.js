function BicycleEditParking() {
    var _this = this;

    this.map = null;
    this.marker = null;
    this.init = function () 
    {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
        _this.initPhoto()
    }

    this.initializeMap = function () 
    {
        var latLng = new google.maps.LatLng(48.9117731, 24.717129);
        if ($("#Position").val() != "") 
        {
            var position = $("#Position").val().replace('(', '').replace(')', '');
            var lat = position.replace(/\s*\,.*/, ''); // first 123
            var lng = position.replace(/.*,\s*/, ''); // second ,456
            latLng = new google.maps.LatLng(parseFloat(lat), parseFloat(lng));
        };
        var mapOptions = {
            zoom: 16,
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