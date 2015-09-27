function Route()
{
    var _this = this;

    this.map = null;
    this.selectedPolylines = null;

    this.init = function ()
    {
        mapMain.init();
        mapDirection.init();
    }
}

var route = null;
$(function () {
    route = new Route();
    route.init();
});