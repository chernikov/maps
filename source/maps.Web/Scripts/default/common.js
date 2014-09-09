function Common() {
    var _this = this;

    this.init = function () {
        $('.dropdown-toggle').dropdown();

        $(document).on("click", ".delete-action",function () {
            return confirm("Действительно удалять?");
        });

        $("#SelectCityID").change(function () {
            $("#SelectCitiesForm").submit();
        });
    };
}

var common = null;
$(function () {
    common = new Common();
    common.init();
});