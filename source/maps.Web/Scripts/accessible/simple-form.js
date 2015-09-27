function SimpleForm() {
    var _this = this;

    this.init = function() {
        $("#SavePutBtn").click(function() {

            var data = JSON.stringify($("#SimpleForm").serializeObject());
            var id = $("#ID").val();
            $.ajax({
                type: "PUT",
                url: "/api/BycicleParking/" + id,
                data: data,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (data)
                {
                    alert(data);
                }
            });
            return false;
        });

        $("#RemoveBtn").click(function() {
            
            var id = $("#ID").val();
            $.ajax({
                type: "DELETE",
                url: "/api/BycicleParking/" + id,
                success: function (data) {
                    alert(data);
                }
            });
            return false;
        });
    }
}

var simpleForm = null;
$().ready(function() {
    simpleForm = new SimpleForm();
    simpleForm.init();
});

