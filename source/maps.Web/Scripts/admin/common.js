function Common() {
    var _this = this;

    this.init = function () {

        $(document).on("click", ".delete-action, .btn-danger", function () {
            return confirm("Ви дійсно хочете видалити?");
        });

        $("#SelectCityID").change(function () {
            $("#SelectCitiesForm").submit();
        });
    };
}

var common;
$(function ()
{
    common = new Common();
    common.init();
});
