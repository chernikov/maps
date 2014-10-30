function UploadSample() {

    var _this = this;

    this.init = function () {
        $("#AddPhoto").fineUploader({
            element: $('#AddPhoto'),
            request: {
                endpoint: "/api/upload"
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
         if (responseJSON.success)
         {
             $.each(responseJSON.files, function (index, item) {
                 var obj = $("<img>").attr("src", item.Value);
                 $("#Wrapper").append(obj);
             });
         }
     }).on('submit', function () {
         $(".qq-upload-fail").remove();
         return true;
     });
    };
}

var uploadSample = null;
$(function () {
    uploadSample = new UploadSample();
    uploadSample.init();
});