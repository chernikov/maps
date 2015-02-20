function ReportCreate()
{
    var _this = this;
    this.init = function ()
    {
        $('#DateTime').datetimepicker({
            format: 'DD.MM.YYYY HH:mm',
            maxDate: new Date(),
        });
    }
}

var reportCreate;

$(function ()
{
    reportCreate = new ReportCreate();
    reportCreate.init();

});