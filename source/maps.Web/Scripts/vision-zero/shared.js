function Shared()
{
    var _this = this;

    this.init = function () {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
            var identifier = $("#Identifier").val();
            mapVisualization.showData(identifier);
            _this.showFilters(identifier);
        });

        $(document).on("click", "#SubmitFilter", function () {
            var id = $(this).data("id");
            var data = $("#FilterForm").serialize();
            mapVisualization.filterData(id, data);
            return false;
        });
    }

    this.showFilters = function (id) {
        $.ajax({
            type: "GET",
            url: "/vision-zero/Home/Filter",
            data: {
                id: id
            },
            success: function (data) {
                $("#FilterWrapper").html(data);
                $(".datepicker").datepicker();
            }
        });
    }
}

var shared = null;
$(function () {
    shared = new Shared();
    shared.init();
});