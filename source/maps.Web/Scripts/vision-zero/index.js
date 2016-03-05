﻿function Index()
{
    var _this = this;

    this.init = function () {
        _this.initUpload();

        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
        });

        $(document).on("click", ".visualizationItem", function () {

            var id = $(this).data("id");
            mapVisualization.showData(id);
            _this.showFilters(id);
        });

        $(document).on("click", "#SubmitFilter", function () {
            var id = $(this).data("id");
            var data = $("#FilterForm").serialize();
            mapVisualization.filterData(id, data);
            return false;
        });

        $(document).on("click", "#ShakePointsBtn", function () {
            var id = $(this).data("id");
            $.ajax({
                type: "GET",
                url: "/vision-zero/Home/Shake",
                data: { id: id },
                success: function (data) {
                    if (data.result == "ok") {
                        mapVisualization.showData(id);
                        _this.showFilters(id);
                    }
                }
            });
            return false;
        });
    }

    this.initUpload = function () {
        $("#Upload").fineUploader({
            element: $('#Upload'),
            request: {
                endpoint: "/vision-zero/Home/UploadFile"
            },
            template: 'qq-template-bootstrap',
            allowedExtensions: ['csv'],
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
                $.ajax({
                    type: "GET",
                    url: "/vision-zero/Home/List",
                    success: function (data) {
                        $("#ListWrapper").html(data);
                    }
                })
            } else {
                alert(responseJSON.error);
            }
        })
        .on('submit', function () {
            $(".qq-upload-fail").remove();
            return true;
        });
    }

    this.showFilters = function (id) {
        $.ajax({
            type: "GET",
            url: "/vision-zero/Home/Filter",
            data: {
                id : id
            },
            success: function (data)
            {
                $("#FilterWrapper").html(data);
                $(".datepicker").datepicker();
            }

        })
    }


}

var index = null;
$(function () {
    index = new Index();
    index.init();
});