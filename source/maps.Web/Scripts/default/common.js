function Common() {
    var _this = this;

    this.init = function () {
        $('.dropdown-toggle').dropdown();

        $('.delete-action').live('click', function () {
            return confirm("Действительно удалять?");
        });
    };
}

var common = null;
$(function () {
    common = new Common();
    common.init();
});