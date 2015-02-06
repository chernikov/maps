function Index() {
    var _this = this;
    this.marker;
    this.distanceWidget;
    this.markers = [];
    this.infoWindows = [];

    this.result = null;
    this.init = function ()
    {
        google.maps.event.addDomListener(window, 'load', function () {
            mapMain.init();
            _this.distanceWidget = new DistanceWidget(mapMain.map);
            google.maps.event.addListener(_this.distanceWidget, 'distance_changed', function () {
                console.log('Position: ' + _this.distanceWidget.get('position') + ', distance: ' + _this.distanceWidget.get('distance'));
            });

            google.maps.event.addListener(_this.distanceWidget, 'position_changed', function () {
                console.log('Position: ' + _this.distanceWidget.get('position') + ', distance: ' + _this.distanceWidget.get('distance'));
            });
        });

        $('#StartDate').datetimepicker({
            format: 'DD.MM.YYYY HH:mm',
            maxDate: new Date(),
        });
        $('#EndDate').datetimepicker({
            format: 'DD.MM.YYYY HH:mm',
            maxDate: new Date(),
        });

        $("#SearchBtn").click(function () {
            _this.clearAll();
            var position = _this.distanceWidget.get('position');
            var distance = _this.distanceWidget.get('distance');
            var ajaxData = {
                lat: position.lat(),
                lng: position.lng(),
                radius :  Math.round(distance * 1000),
                minTime: moment($('#StartDate').val(), "DD.MM.YYYY HH:mm").format("YYYY-MM-DD HH:mm"),
                maxTime: moment($('#EndDate').val(), "DD.MM.YYYY HH:mm").format("YYYY-MM-DD HH:mm")
            };
            $.ajax({
                type: "POST",
                url: "/instagram/Home/Get",
                data: ajaxData,
                beforeSend : function() {
                    $.blockUI({message : "Зачекайте хвильку..."});
                },
                success: function (data) {
                    if (data.result == "success")
                    {
                        _this.result = data.data;
                        _this.enableList();

                        $.each(data.data, function (i, item)
                        {
                            _this.createMarker(item);
                        });
                    }
                },
                complete: function () {
                    $.unblockUI();
                }

            });
            return false;
        });

        $("#ListBtn").click(function ()
        {
            $.ajax({
                type: "POST",
                url: "/instagram/Home/EmptyModal",
                success: function (data) {
                    var obj = $(data);
                    $("#PopupWrapper").html(obj);

                    $.each(_this.result, function (i, item) {
                        _this.createThumb(item);
                    });
                    obj.modal({
                        backdrop: false,

                    });
                }
            });
        });

        _this.enableList();
    }

    this.createMarker = function (item)
    {
        var latLng = new google.maps.LatLng(parseFloat(item.Lat), parseFloat(item.Lng));

        var image = {
            url: "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAACXBIWXMAAAsTAAALEwEAmpwYAAAD5klEQVRIx9WWzYtWVRzHP/d5m2ceZxpfx7eNpTkLwwxJsNpFK6k2A0WLVv4FRlYkRepCEt1Ei1okKUSFLRSkaOFGCSkFaWNiEaM+4zijM8/bveee11+Lc8dJnmZq46IDh3vhwO9zvt/v75x7ExHhUY4Sj3hUllqcOXWoLEFekRDGg/dj4sIO7xzBuqve2OveutPBuLNjBz/3i9VIFrNo5tShMRE5Wdv24q6BFaso1waoDDYgBEx7DtOeo/vHdTpXL/7srXvzqaMnr/9nwPSXB/eBHF62e3ywPjSEtGcRlSLde4jRUBkkqVZJlq+m3bzJ5PffKW/dgWc+/fb4vwKmvvjw48bOPW8Prh4lmZtBevcQp8E5xFowBtEa0RqsIRleQdiwiZmrl7l78fzRXad+2L8oYPKz91+rP/3S14+tWUu41wTbhRDA+1jcWsQYKACi8wgEypu3MXH5EjOXLrz+wrnL3/R10a1P3qkF548N1muEyRuQ3QejF3ZrdLRH58j8+7yaTht35QKrRlfjVXr4/HOban2AYN34wJYdG8utaSRrIblCVAZ5hqg4UQrJcyRXkOdInkOuQGVIt0391g1WPLl1i8/S8b429dbuqXmP9GbBaUQk2hMCOI84C9ZFi4xBtIlqtIqbyTMQz0ilRDNL9wBfPQRw2u4sG00wPfDuQXEpMsD9PQf7cHGVRYA1NBo1Qq529yvQZl3JZEivCHZegfeID0UXOXA2ZvIQIAWtEGuoVpfhczb2AZw2yOwdpNVdsEYEfIgA7xFriuCj95JnxYwgrEayIYLF/IMCO2UzNVLWGpyHMK/AIc7FpzFgcsQUAWuF6Dzao3OEgDaKYGj+k0VXVK83NmwsQeUQfGFPLI4rzoA1ETBvk8kftHBSLZGqFHFc6bco7Z5rTYU3hoYbSNYF5xYAzsYMXNFB1sRzYPUDKMFDo8H0nVkQzvUDet3T91tzHy3fvH5LQ2eIygtrbAS5QoU1EWhN7CpvEecoVcu0xTM1wZ/A6b6D9vyZS8b1Ogfu3ryN4AgqRXptpNdFeh0k7URlKkWytLgm8qggAYbqNJttgPfGnZhFL7sft686Nrp+5b6NI8uodLqEVMXd+mjZQjYeRCg1aphGnYnpFrd/5/i4l7eW/OD4rLe/eW129pbjgye2rq2tq1fBevAGMQ4kQAlKtTLSGOSOyvjtp5YBDgJHlryukyQpEQWXToywdUQ4MbqWZ9esWc4AMDxQBhG6mSEnMD2ZMTXJLx3YuzdwDQiAiEhYDFArVFWAZENC9d0Kr66El8uBxyvCdgAHv7rAxH04cwTOToEBJC7hRJbI4H/3V/EXWgIvv5o6njUAAAAASUVORK5CYII=",
            // This marker is 20 pixels wide by 32 pixels tall.
            size: new google.maps.Size(24, 24),
            // The origin for this image is 0,0.
            origin: new google.maps.Point(0, 0),
            // The anchor for this image is the base of the flagpole at 0,32.
            anchor: new google.maps.Point(12, 12)
        };

        var marker = new google.maps.Marker({
            map: mapMain.map,
            position: latLng,
            icon: image,
        });
        marker.set("Id", item.Id);
        marker.set("PhotoUrl", item.PhotoUrl);
        marker.set("Link", item.Link);
        google.maps.event.addListener(marker, 'click', function () {
            _this.showInfo(marker);
        });
        _this.markers.push(marker);
    }

    this.createThumb = function (item)
    {
        var list = $("#ModalWrapper .row");
        list.append("<div class=\"col-md-3\"><div class=\"thumbnail instagram-thumbnail\"><a href=\"" + item.Link + "\" target=\"_blank\"><img src=\"" + item.PhotoUrl + "\"  /></a></div></div>");
    }

    this.showInfo = function (marker) {
        var id = marker.get("Id");
        var photoUrl = marker.get("PhotoUrl");
        var link = marker.get("Link");
        _this.clearAllInfo();
        var data = "<div class=\"instagram-photo\"><a href=\"" + link + "\" target=\"_blank\"><img src=\"" + photoUrl + "\" /></a></div>";
        var infoWindow = new google.maps.InfoWindow({
            content: data
        });
        infoWindow.open(mapMain.map, marker);
        _this.infoWindows.push(infoWindow);
    }


    this.clearAllInfo = function () {
        for (var i = 0; i < _this.infoWindows.length; i++) {
            _this.infoWindows[i].setMap(null);
        };
        _this.infoWindows = [];
    }

    this.clearAll = function() {
        $.each(_this.markers, function (i, marker) {
            marker.setMap(null);
        });
        _this.clearAllInfo();
    }

    this.enableList = function () {
        if (_this.result != null && _this.result.length > 0)
        {
            $("#ListBtn").removeAttr("disabled");
        } else {
            $("#ListBtn").attr("disabled", "disabled");
        }
    }
}

var index = null;
$(function () {
    index = new Index();
    index.init();
});