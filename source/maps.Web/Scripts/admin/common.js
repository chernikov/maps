function Common() {
    var _this = this;

    this.init = function () {
        $(".delete-action, .btn-danger").live("click", function () {
            return confirm("Вы действительно хотите удалить?");
        });
    };
}

var common;
$(function ()
{
    common = new Common();
    common.init();
});
