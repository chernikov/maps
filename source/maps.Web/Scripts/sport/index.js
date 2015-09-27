function Index() {
    var _this = this;

    this.init = function ()
    {
        $("#ChangePhoto").fineUploader({
            element: $('#ChangePhoto'),
            request: {
                endpoint: "/File/UploadAvatar"
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
        }).on('complete', function (event, id, filename, responseJSON) {
            if (responseJSON.success) {
                $("#PhotoUrl").val(responseJSON.fileUrl);
                $("#ImagesWrapper").show();
                $(".trueimage").attr("src", responseJSON.fileUrl + "?w=300&h=300&mode=crop");
            }
        }).on('submit', function () {
            $(".qq-upload-fail").remove();
            return true;
        });

        $(".saveBtn").click(function () {
            var PhotoUrl = $("#PhotoUrl").val();
            var AvatarType = $(this).data("type");
            var url = "/sport/home/download?PhotoUrl="+PhotoUrl + "&AvatarType="+ AvatarType;
            window.open(url, '_blank');
            return false;
        });
    }

}

var index = null;
$(function () {
    index = new Index();
    index.init();
})