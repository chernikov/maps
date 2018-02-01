function Index()
{
    var _this = this;

    this.init = function () {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init(function () {
                mapVisualization.showData();
            });
        });

        $(document).on("click", "#SubmitFilter", function ()
        {
            mapVisualization.filterData($("#StartYear").val(), $("#LastYear").val(), $("#ExistOnStartYear").val());
            return false;
        });
    }
}

var index = null;
$(function () {
    index = new Index();
    index.init();
});