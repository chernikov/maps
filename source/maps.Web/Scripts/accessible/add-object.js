function AddObject() {
    var _this = this;

    this.geocoder = null;
    this.marker = null;
    this.init = function () {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init(function () {
                mapMain.map.setOptions({ draggableCursor: 'crosshair' });
                _this.geocoder = new google.maps.Geocoder();
                google.maps.event.addListener(mapMain.map, 'click', _this.clickOnMap);
            });
        });
        $(document).on("click", "#SaveObjectBtn", function () {
            _this.saveObject();
        });
    }

    this.clickOnMap = function (event) {
        _this.addMarker(event.latLng);
    }

    this.addMarker = function (position) {
        _this.marker = new google.maps.Marker({
            map: mapMain.map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: position,
        });
        _this.createPopup(position);
    }

    this.createPopup = function (position) {
        //показать попап для добавления
        $.ajax({
            type: "GET",
            url: "/accessible/Object/Create",
            success: function (data) {
                var obj = $(data);
                $("#PopupWrapper").html(obj);
                obj.modal({
                    backdrop: false,
                });

                _this.initModal(position);
            }
        });
    }
    this.initModal = function (position) {
        $(".chzn-select").chosen({ disable_search_threshold: 10 });

        $('.tooltipInfo').tooltip({
            html: true
        });

        if (position != null) {
            $("#Lat").val(position.lat());
            $("#Lng").val(position.lng());
            //Address
            _this.geocoder.geocode({ 'latLng': position }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if (results[0]) {
                        $("#Address").val(results[0].formatted_address);
                    }
                } else {
                    console.error("Geocoder failed due to: " + status);
                }
            });
        }
        $('#IssueModal').on('hidden.bs.modal', function (e) {
            _this.clearMarker();
        });

        $("#AddPhoto").fineUploader({
            element: $('#AddPhoto'),
            request: {
                endpoint: "/Accessible/Object/Upload"
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
            _this.createPhoto(responseJSON);
        }).on('submit', function () {
            $(".qq-upload-fail").remove();
            return true;
        });

        $(document).on("click", ".RemoveUtilityPhoto", function () {
            $(this).closest(".UtilityPhotoItem").remove();
        });

        $("#CloseThanksBtn").click(function () {
            window.location = "/utility";
        });
    }

    this.createPhoto = function (responseJSON) {
        var id = responseJSON.utilityPhoto.ID;
        $.ajax({
            type: "GET",
            url: "/utility/UtilityPhoto/Item",
            data: { id: id },
            success: function (data) {
                $("#UtilityPhotosWrapper").append(data);
            }
        });
    }

    this.saveObject = function () {
        $.ajax({
            url: "/accessible/Home/Edit",
            type: "POST",
            data: $("#IssueForm").serialize(),
            success: function (data) {
                $("#PopupWrapper").empty();
                var obj = $(data);
                $("#PopupWrapper").html(obj);
                obj.modal({
                    backdrop: false,
                });
                _this.initModal();
            }
        });
    }

    this.clearMarker = function () {
        if (_this.marker != null) {
            _this.marker.setMap(null);
            _this.marker = null;
        }
    }

}
var addObject = null;
$(function () {
    addObject = new AddObject();
    addObject.init();
})