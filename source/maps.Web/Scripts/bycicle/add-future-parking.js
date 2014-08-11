function AddFutureParking() {
    var _this = this;

    this.map = null;
    this.marker = null;
    this.geocoder = null;
    this.init = function () {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
        $(document).on("click", "#SaveFutureParkingBtn", function () {
            _this.saveParking();
        });
        $("#ActionWrapper").hide();
        _this.geocoder = new google.maps.Geocoder();
    }

    this.initializeMap = function () {
        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(centerLat, centerLng),
            draggableCursor: 'crosshair'
        };
        _this.map = new google.maps.Map(document.getElementById('map'), mapOptions);
        google.maps.event.addListener(_this.map, 'click', _this.clickOnMap);
    }

    this.clickOnMap = function (event)
    {
        _this.addParkingMarker(event.latLng);
    }

    this.addParkingMarker = function (position)
    {
        _this.marker = new google.maps.Marker({
            map: _this.map,
            draggable: true,
            animation: google.maps.Animation.DROP,
            position: position,
        });

        //показать попап для добавления
        $.ajax({
            type: "GET",
            url: "/bicycle/Parking/PopupFuture",
            success: function (data)
            {
                var obj = $(data);
                $("#PopupWrapper").html(obj);
                obj.modal({
                    backdrop: false
                });
                _this.initModal(position);
                $("#Position").val(position);

                $('#ParkingModal').on('hidden.bs.modal', function (e) {
                    _this.clearMarker();
                });
            }
        })
    }

    this.initModal = function (position) {
        $('.tooltipInfo').tooltip({
            html : true
        });
        //CenterDistance
        var center = new google.maps.LatLng(centerLat, centerLng);
        var distance = position.distanceFrom(center);
        $("#CenterDistance").val(distance);
        //Address
        _this.geocoder.geocode({ 'latLng': position }, function (results, status)
        {
            if (status == google.maps.GeocoderStatus.OK)
            {
                if (results[0])
                {
                   $("#Address").val(results[0].formatted_address);
                }
            } else {
                console.error("Geocoder failed due to: " + status);
            }
        });
    }

    this.saveParking = function () 
    {
        $.ajax({
            url: "/bicycle/Parking/SaveFutureParking",
            type: "POST",
            data: $("#FutureParkingForm").serialize(),
            success: function (data) {
                if (data.result == "ok")
                {
                    _this.clearMarker();
                    $("#PopupWrapper").empty();
                    window.location = "/bicycle/Parking";
                }
            }
        });
    }


    this.clearMarker = function ()
    {
        _this.marker.setMap(null);
        _this.marker = null;
    }

}

var addFutureParking = null;

$(function () {
    addFutureParking = new AddFutureParking();
    addFutureParking.init();
})