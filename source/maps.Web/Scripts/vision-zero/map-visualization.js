function MapVisualization()
{
    var _this = this;
    this.infoWindows = [];
    this.markers = [];
    this.failedItems = [];
    this.markerClusterer = null;

    this.showData = function (id)
    {
        _this.clearAll();
        $.ajax({
            type: "GET",
            url: "/vision-zero/Home/ShowData",
            data : {
                id : id
            },
            success: function (data) {
                _this.outputMarkers(data);
            }
        });
    }

    this.filterData = function (id, filter) {
        _this.clearAll();
        $.ajax({
            type: "GET",
            url: "/vision-zero/Home/FilterData",
            data: filter,
            success: function (data) {
                _this.outputMarkers(data);
            }
        });
    }

    this.outputMarkers = function (data)
    {
        if (data.result == "ok")
        {
            $.each(data.data, function (i, item)
            {
                if (item.lat != 0 && item.lng != 0) {
                    var latLng = new google.maps.LatLng(parseFloat(item.lat), parseFloat(item.lng));
                    var marker = new google.maps.Marker({
                        map: mapMain.map,
                        position: latLng,
                        anchor : new google.maps.Point(-8, -8)
                    });
                    if (item.accuracy) {
                        if (item.accuracy > 8) {
                            marker.setIcon("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAW5QTFRFgn15gn15gn15gn15gn15gn15gn15mZWSgn15wL+8wcC9xMLAxMPA+fz5+v36+/36+/37AGYAAGcAAWgAAWkAAWoAAmwAAm0AA3AAA3EABHIABHQABHUABWsFBXUABXcABnkABnoABnsAB28FB30AB34ACIIACYMACYQACYUACoYACogAC4oAC4sADI4ADZABD5MDE3ITFXQUFnYUF54NGaAPHHsbJn0mJ4ElMYwtM4YyQZ47QaM5Qps+QqI7Q509RJJDRJdBRJdCRJk/RJlARJ0/RKM9RZZDRZdCRZlBRpVERpZERp1BRp9BR5RGR5VGR5dER5dGR5pDSKVBSZRISZ1FSpVJS5RLTJZLTZVNT5ZPUZdRcblrcrdud7pzerl2g7WDhLiDktSOldiSotSeo9Wfps2mqcyortKtsNitsdutudm3v9q+wuK/w9/BxOLB4+Lh7/bv8Pjv8fnw8/ny8/nz8/rz9Pn0+Pf3+fn5/gAfdgAAABF0Uk5TACBGjI2jx+Tw8PD5+f7+/v64k2s4AAAA3UlEQVQY013PP0pDQRAH4Jnfzszug4B/iojgHQIieAdbazttbAQPItikMTew9TDaRISgYISHEvAluztrYyF+N/iYiAgAyN2diJiIIXqeaJjl4o2YWPQqr3veiXKbSxOC6vKxIS7T2MgrQ68/nlha0oit3Zss0PoSBcECeLWtle2yzbX1m7ph1iOZCtJr+urfVhCAu2MIES/eP2sQC4FHROLD6Lk6JGoIfDA4fGbNWzAVU+3uHJ7TSWMAEuwwFmcKJqcPlqLa5L6sq5AX2j/7XsS9zkr235xc/Mn96/8AfPBaWDO+Xu4AAAAASUVORK5CYII=");
                        } else {
                            marker.setIcon("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAapQTFRFgn15gn15gn15gn15gn15gn15gn15mZWRmZWSmpWSmpaSgn15wb26wb66wb+7wr+7wr+8xMG+xMK+xcG+xcK+xcK/ygAAywAAywIAzAAAzAUAzAUFzAgFzggAzwsAzwwA0A0A0Bsb0hIA0xMA0xQA0xUA1CET1RkA1S4m1TUy1hoA1iYU1x0A1x4A1y0t2CAA2CkU2END2SEA2UZG2UlI2UlJ2iMA2iQA2kJC2kND2kRE2kZG2ktL2k1L2yUA2zol20JC3CcA3D8/3EBA3EFB3END3FJN3SoA3VZP3iwA3i0A3j4+3kVF3llR3y8B3zAB3z093z8/4DAB4DIC4Ds74EFB4jcH4zgJ4zs741xB5Dk55DsN5DwP5D095OHh5OLh5T8T5kIY5oOD52RB54aD6EYe6Ecg6GZC6HZ26W5u6XNz6k0q6mtr608t7FMz7FM07G9O7aam7amo8F1D8F5G8re387it9J6e9J+f9MS+9cHB97+/98HB98K0+Pf3+bSp+ca8+fn5+rev/O/v/PDv/fPz/fX0/vLy/vPz/vTy/vX0/vn4/vr6//z7Ga4jrAAAABZ0Uk5TACBGjI2jx+fn5+fw9PT09PT8/Pz8/LX1318AAADvSURBVBjTY2AAAiYWNm5uNhYmBghgZOYSja+rixflZGYE81kFGturczKrWhsFWEEizAI95VEh3s4OVoU9AsxA/ZyNFREBThZWxqY2RY0cTAwsoh2xgS5Wxia2bh6+baIsDGzxNaGu1oaaGmraBvaV8WwM3HVZPpaKMvIqWvpm5pF13ECBDD1pCTlVXTtP/+B0oABbfKmkmKyyjmNQeEJiLVALi3CzuISCurlfdFpuXqcQCwMTe2OSlJKue1hqfkk9yFoGZt7uFBUjr5js4qZePmaw03kaW8qSCxq6GvlZIZ5hZheMA3pOhAPiOTTvAwA9pDN9YEJXYgAAAABJRU5ErkJggg==");
                        }
                    }
                    marker.set("Id", item.Id);
                    google.maps.event.addListener(marker, 'click', function () {
                        _this.showInfo(marker);
                    });
                    _this.markers.push(marker);
                } else {
                    _this.failedItems.push(item);
                }
            });
            //_this.markerClusterer = new MarkerClusterer(mapMain.map, _this.markers,
            //{
            //    maxZoom: 18,
            //    gridSize: 10,
            //    styles: null
            //});
            $("#CountWrapper").text(_this.markers.length + " items");
            $("#CountFailedWrapper").text(_this.failedItems.length + " failed items");
        }
    }

    this.clearAll = function ()
    {
        for (var i = 0; i < _this.markers.length; i++) {
            _this.markers[i].setMap(null);
        }
        _this.markers = [];
        _this.failedItems = [];

        if (_this.markerClusterer != null) {
            _this.markerClusterer.clearMarkers();
        }
    }

    this.showInfo = function (marker) {
        var id = marker.get("Id");
        $.ajax({
            type: "GET",
            url: "/vision-zero/Home/Info",
            data: { id: id },
            success: function (data) {
                _this.clearAllInfo();
                var infowindow = new google.maps.InfoWindow({
                    content: data
                });
                _this.infoWindows.push(infowindow);
                infowindow.open(mapMain.map, marker);
            }
        });
    }

    this.clearAllInfo = function () {
        for (var i = 0; i < _this.infoWindows.length; i++) {
            _this.infoWindows[i].setMap(null);
        }
        _this.infoWindows = [];
    }
}


var mapVisualization = null;
$(function ()
{
    mapVisualization = new MapVisualization();
});