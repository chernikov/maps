function Index() {
    var _this = this;

  
    this.init = function ()
    {
        $(document).on("click", "#CreateGoalBtn", function () {
            $.ajax({
                type: "POST",
                url: "/goal/Home/Create",
                data: $("#CreateGoalForm").serialize(),
                success: function (data)
                {
                    $("#ItemWrapper").html(data);
                }
            });
            return false;
        });


        $(document).on("click", ".btn-danger", function () {
            return confirm("Ви дійсно хочете видалити?");
        });

        $(".cell.active").click(function () {
            _this.ProcessClick($(this));
        });
    }

    this.ProcessClick = function (item) {
        var id = item.data("id");

        $.ajax({
            type: "POST",
            url: "/goal/Home/Set",
            data: { id: id },
            success: function (data) {
                if (data.result == "ok")
                {
                    if (data.value == "set")
                    {
                        item.removeClass("empty");
                        item.addClass("set");
                        $("#Progress").text(data.progress);
                    };
                    if (data.value == "empty")
                    {
                        item.removeClass("set");
                        item.addClass("empty");
                        $("#Progress").text(data.progress);
                    };
                }
            }
        });
    }
}

var index = null;

$(function () {
    index = new Index();
    index.init();
})