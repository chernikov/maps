function RouteIndex() {
    var _this = this;

    this.init = function () 
    {
        $("#ProcessRoutesBtn").click(function () {
            $(this).attr("disabled", "disabled");
        });
    }
}

var routeIndex = null;
$(function ()
{
    routeIndex = new RouteIndex();
    routeIndex.init();
})