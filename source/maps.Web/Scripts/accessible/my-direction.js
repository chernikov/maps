function MyDirection() {
    var _this = this;
    this.init = function () {
        mapMain.init();
        mapDirection.loadMyRoutes();
    }
}

var myDirection = null;
$(function () {
    myDirection = new MyDirection();
    myDirection.init();
});