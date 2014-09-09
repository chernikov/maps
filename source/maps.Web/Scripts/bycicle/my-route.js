function MyRoute() {
    var _this = this;
    this.init = function () {
        mapMain.init();
        mapRoute.loadMyRoutes();
    }
}

var myRoute = null;
$(function () {
    myRoute = new MyRoute();
    myRoute.init();
});