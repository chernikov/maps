function AddParking() {
    var _this = this;

    this.geocoder = null;
    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init(function ()
            {
                mapMain.map.setOptions({ draggableCursor: 'crosshair' });
                _this.geocoder = new google.maps.Geocoder();
                google.maps.event.addListener(mapMain.map, 'click', _this.clickOnMap);
            });
        });
        $(document).on("click", "#SaveParkingBtn", function ()
        {
            _this.saveParking();
        });

        $("#ActionWrapper").hide();
    }

    this.clickOnMap = function (event)
    {
        _this.addParkingMarker(event.latLng);
    }

    this.addParkingMarker = function (position)
    {
        _this.marker = new google.maps.Marker({
            map: mapMain.map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: position,
        });

        //показать попап для добавления
        $.ajax({
            type: "GET",
            url: "/bicycle/parking/popup",
            success: function (data)
            {
                var obj = $(data);
                $("#PopupWrapper").html(obj);
                obj.modal({
                    backdrop: false,

                });
                _this.initModal(position);
                $("#Position").val(position);

                $('#ParkingModal').on('hidden.bs.modal', function (e) {
                    _this.clearMarker();
                });
            }
        })
    }

    this.initModal = function (position)
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
       .on('complete', function (event, id, filename, responseJSON)
       {
           if (responseJSON.success)
           {
               $("#PhotoUrl").val(responseJSON.fileUrl);
               $("#ParkingImage").attr("src", responseJSON.fileUrl + "?w=300&h=300&mode=crop");
           }
       }).on('submit', function () {
           $(".qq-upload-fail").remove();
           return true;
       });

        $('.tooltipInfo').tooltip({
            html : true
        });
        //CenterDistance
        var center = new google.maps.LatLng(mapMain.centerLat, mapMain.centerLng);
        var distance = position.distanceFrom(center);
        $("#CenterDistance").val(distance);
        //Address
        _this.geocoder.geocode({ 'latLng': position }, function (results, status)
        {
            if (status == google.maps.GeocoderStatus.OK)
            {
                if (results[0])
                {
                   $("#Address").val(results[0].formatted_address);
                }
            } else {
                console.error("Geocoder failed due to: " + status);
            }
        });
    }

    this.saveParking = function () 
    {
        $.ajax({
            url : "/bicycle/Parking/SaveParking",
            type: "POST",
            data: $("#ParkingForm").serialize(),
            success: function (data)
            {
                if (data.result == "ok")
                {
                    _this.clearMarker();
                    $("#PopupWrapper").empty();
                    window.location = "/bicycle/Parking";
                }
            }
        });
    }

    this.clearMarker = function ()
    {
        _this.marker.setMap(null);
        _this.marker = null;
    }

}

var addParking = null;

$(function () {
    addParking = new AddParking();
    addParking.init();
})