function EditIssue() {
    var _this = this;

    this.geocoder = null;
    this.marker = null;
    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapSmallMain.init(function () {
                mapSmallMain.map.setOptions({ draggableCursor: 'crosshair' });
                _this.geocoder = new google.maps.Geocoder();
                google.maps.event.addListener(mapSmallMain.map, 'click', _this.clickOnMap);

                if ($("#Lat").val() != "0" && $("#Lng").val() != "0") {
                    var position = new google.maps.LatLng(parseFloat($("#Lat").val()), parseFloat($("#Lng").val()));
                    _this.changeMarker(position, false);
                }
            });
        });

        $(".chzn-select").chosen({ disable_search_threshold: 10 });
        $('.tooltipInfo').tooltip({ html: true });

        if ($("#UtilityDepartmentName").length > 0) {
            $("#UtilityDepartmentName").typeahead({
                hint: false,
                highlight: true,
                minLength: 2
            },
            {
                name: 'departments',
                displayKey: 'name',
                source: _this.getDepartments()
            });
        }
        
        $("#AddPhoto").fineUploader({
            element: $('#AddPhoto'),
            request: {
                endpoint: "/Utility/UtilityPhoto/Upload"
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
            $(this).closest(".UtilitPhotoItem").remove();
        });
    }

    this.clickOnMap = function (event) {
        _this.changeMarker(event.latLng, true);
    }

    this.changeMarker = function (position, force) {

        if (_this.marker != null)
        {
            _this.clearMarker();
        }
        _this.marker = new google.maps.Marker({
            map: mapSmallMain.map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: position,
        });
        google.maps.event.addListener(_this.marker, 'dragend', _this.markerPositionChanged);
        _this.markerPositionChanged(force);
    }

    this.markerPositionChanged = function (force) {
        var position = _this.marker.position;

        $("#Lat").val(position.lat());
        $("#Lng").val(position.lng());

        if (force)
        {
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
    }

    this.createPhoto = function (responseJSON) {
        var id = responseJSON.utilityPhoto.ID;
        $.ajax({
            type: "GET",
            url: "/utility/UtilityPhoto/Item",
            data: { id: id },
            success: function (data) {
                var obj = $(data);
                var state = $("#Status").val();
                $(".PhotoItemState", obj).val(state);
                $("#UtilityPhotosWrapper").append(obj);
            }
        });
    }


    this.clearMarker = function () {
        _this.marker.setMap(null);
        _this.marker = null;
    }

    this.getDepartments = function ()
    {
        return function findMatches(q, cb)
        {
            var matches, substrRegex;
            $.ajax({
                url: "/utility/Department/Get",
                data: { str: q },
                async : false,
                success: function (data)
                {
                    cb(data.data);
                }
            });
        };
    };
}

var editIssue = null;
$(function () {
    editIssue = new EditIssue();
    editIssue.init();
})