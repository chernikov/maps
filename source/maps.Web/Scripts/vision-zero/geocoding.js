function Geocoding() {
    var _this = this;
    
    var addresses = [];

    var askUrl = "https://maps.googleapis.com/maps/api/geocode/json?key=AIzaSyBxdpYRxJgtDdNFXPh-o3Jcsi7f1mSm4pk&address="

    this.init = function ()
    {
        $("#ProcessBtn").click(_this.Process);

    }

    this.Process = function ()
    {
        var strings = $("#Input").val().split("\n");
        _this.ProcessItem(strings);
    }

    this.ProcessItem = function (strings) {
        if (!strings.length) {
            var results = _.map(addresses, function (address) {
                return address.latlng;
            });
            var str = results.join("\n");
            $("#Output").val(str);
            return;
        }
        var item = strings[0];

        var findIn = _.find(addresses, function (value) {
            value.address = item;
        });
        if (findIn) {
            addresses.push({
                address: item,
                latlng: findIn.latlng
            });
            _this.ProcessItem(strings.splice(1));
        } else {
            $.ajax({
                type: "GET",
                url: askUrl + item,
                success: function (data) {

                    if (data.results.length) {
                        var result = data.results[0].geometry;
                        if (result) {
                            var latlng = result.location.lat + "\t" + result.location.lng;
                            addresses.push({
                                address: item,
                                latlng: latlng
                            });
                        }
                        
                    } else {
                        addresses.push({
                            address: item,
                            latlng: "47.5605\t31.336117"
                        });
                    }
                    _this.ProcessItem(strings.splice(1));
                },
                error: function (data) {
                    addresses.push({
                        address: item,
                        latlng: "47.5605\t31.336117"
                    });
                    _this.ProcessItem(strings.splice(1));
                }
            })
        }
    }
}

var geocoding = null;

$(function () {
    geocoding = new Geocoding();
    geocoding.init();
})