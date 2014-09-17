function Item() {
    _this = this;

    this.map = null;
    this.centerLat = null;
    this.centerLng = null;
    this.zoom = null;
    this.marker = null;
    this.init = function () {
        _this.centerLat = $("#map").data("lat");
        _this.centerLng = $("#map").data("lng");
        _this.zoom = $("#map").data("zoom");

        var mapOptions = {
            zoom: _this.zoom,
            center: new google.maps.LatLng(_this.centerLat, _this.centerLng),
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);

        _this.marker = new google.maps.Marker({
            map: _this.map,
            position: new google.maps.LatLng(_this.centerLat, _this.centerLng)
        });

        $(document).on("click", "#WriteCommentBtn", function () {
            _this.writeComment();
            return false;
        });
    }

    this.writeComment = function () {
        $.ajax({
            type: "POST",
            url: "/utility/Comment/Write",
            data: $("#WriteCommentForm").serialize(),
            success: function (data)
            {
                $("#CommentWrapper").html(data);
            }
        });
    }
}

var item = null;
$(function () {
    item = new Item();
    item.init();
});