function Common() {
    var _this = this;

    this.init = function () {
        $('.dropdown-toggle').dropdown();

        

        $(document).ajaxStart(function () {
            $.blockUI({ message: "" });
        });

        $(document).ajaxComplete(function () {
            $.unblockUI();
        });

        $(document).on("click", ".delete-action",function () {
            return confirm("Точно видалити?");
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