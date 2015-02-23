function ReportCreate()
{
    var _this = this;
    this.init = function ()
    {
        $('#DateTime').datetimepicker({
            format: 'DD.MM.YYYY HH:mm',
            maxDate: new Date(),
        });

        $("#RouteID").change(function () {
            _this.updateSelectBus();
        });

        $(".RuleSelector").click(function () {
            _this.updateRuleSelector();
        });

        $("#AddPhoto").fineUploader({
            element: $('#AddPhoto')[0],
            request: {
                endpoint: "/Bus/Report/Upload"
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

        $(document).on("click", ".RemoveReportPhoto", function () {
            $(this).closest(".ReportPhotoItem").remove();
        });

        _this.updateRuleSelector();

        $('.tooltipInfo').tooltip({
            html: true
        });
    }

    this.updateSelectBus = function ()
    {
        var id = $("#RouteID").val();

        $.ajax({
            type: "GET",
            url: "/Bus/Report/SelectBus",
            data: { routeId: id },
            success: function (data)
            {
                $("#BusWrapper").html(data);
            }
        });
    }

    this.updateRuleSelector = function () {
        $(".RuleSelector").each(function () {
            $(this).removeAttr("disabled");
        });
        $("#BusID").removeAttr("disabled");
        if ($(".RuleSelector.IsRoute:checked").length > 0) {
            $(".RuleSelector.IsBus").attr("disabled", "disabled");
            $("#BusID").attr("disabled", "disabled");
        }
        if ($(".RuleSelector.IsBus:checked").length > 0)
        {
            $(".RuleSelector.IsRoute").attr("disabled", "disabled");

        }
        
    }

    this.createPhoto = function (responseJSON) {
        var id = responseJSON.reportPhoto.ID;
        $.ajax({
            type: "GET",
            url: "/Bus/Report/Photo",
            data: { id: id },
            success: function (data) {
                $("#ReportPhotosWrapper").append(data);
            }
        });
    }
}

var reportCreate;

$(function ()
{
    reportCreate = new ReportCreate();
    reportCreate.init();

});