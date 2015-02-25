function LoginIndex() {
    var _this = this;

    this.init = function ()
    {
        $("#SendCode").click(function () {
            _this.sendCode();
        });

    }

    this.sendCode = function ()
    {
        $.ajax({
            type: "GET",
            url: "/Bus/Login/SendCode",
            data: { mobile: $("#Mobile").val() },
            success: function (data)
            {
                if (data.result == "ok") {
                    $("#SendCode").addClass("disabled");
                }
            }
        });
    }
}

var loginIndex;

$(function () {
    loginIndex = new LoginIndex();
    loginIndex.init();

});