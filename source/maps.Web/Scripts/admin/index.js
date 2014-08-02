function Index() {
    var _this = this;

    this.init = function () 
    {

        $("#ProcessRoutesBtn").click(function () {
            $(this).attr("disabled", "disabled");
        })
    }

}

var index = null;
$(function ()
{
    index = new Index();
    index.init();
})