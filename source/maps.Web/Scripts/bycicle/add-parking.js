function AddParking() {
    var _this = this;

    this.map = null;
    this.marker = null;
    this.init = function () {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
        $(document).on("click", "#SaveParkingBtn", function () {
            _this.saveParking();
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
        google.maps.event.addListener(_this.map, 'click', _this.clickOnMap);
    }

    this.clickOnMap = function (event)
    {
        _this.addParkingMarker(event.latLng);
    }

    this.addParkingMarker = function (position)
    {
        _this.marker = new google.maps.Marker({
            map: _this.map,
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
                    backdrop: false
                });
                _this.initModal();
                $("#Position").val(position);

                $('#ParkingModal').on('hidden.bs.modal', function (e) {
                    _this.clearMarker();
                });
            }
        })
    }

    this.initModal = function () {
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

        $('.tooltipInfo').tooltip({
            html : true
        });
    }

    this.saveParking = function () 
    {
        $.ajax({
            url : "/bicycle/Parking/SaveParking",
            type: "POST",
            data: $("#ParkingForm").serialize(),
            success: function (data) {
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