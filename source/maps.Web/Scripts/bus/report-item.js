function ReportItem()
{
    var _this = this;
    this.init = function ()
    {
        _this.initAnswerForm();
    }

    this.initAnswerForm = function ()
    {
        if ($("#AnswerBtn").length > 0) {
            $("#AnswerBtn").click(function () {
                _this.answer();
                return false;
            });

        }
    }

    this.answer = function ()
    {
        var ajaxData = $("#AnswerForm").serialize();
        $.ajax({
            type: "POST",
            url: "/Bus/Report/Answer",
            data: ajaxData,
            success: function (data) {
                $("#AnswerWrapper").html(data)
                {
                    _this.initAnswerForm();
                }
            }
        });
    }
}

var reportItem;
$(function ()
{
    reportItem = new ReportItem();
    reportItem.init();
});