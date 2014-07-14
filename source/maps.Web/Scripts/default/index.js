function Index() {
    var _this = this;

    var map;

    var MY_MAPTYPE_ID = 'custom_style';

    this.init = function () {
        google.maps.event.addDomListener(window, 'load', _this.initializeMap);
    }

    this.initializeMap = function () {
        var featureOpts = [
          {
              "featureType": "road.highway",
              "elementType": "geometry",
              "stylers": [
                { "saturation": -100 },
                { "lightness": -8 },
                { "gamma": 1.18 }
              ]
          }, {
              "featureType": "road.arterial",
              "elementType": "geometry",
              "stylers": [
                { "saturation": -100 },
                { "gamma": 1 },
                { "lightness": -24 }
              ]
          }, {
              "featureType": "poi",
              "elementType": "geometry",
              "stylers": [
                { "saturation": -100 }
              ]
          }, {
              "featureType": "administrative",
              "stylers": [
                { "saturation": -100 }
              ]
          }, {
              "featureType": "transit",
              "stylers": [
                { "saturation": -100 }
              ]
          }, {
              "featureType": "water",
              "elementType": "geometry.fill",
              "stylers": [
                { "saturation": -100 }
              ]
          }, {
              "featureType": "road",
              "stylers": [
                { "saturation": -100 }
              ]
          }, {
              "featureType": "administrative",
              "stylers": [
                { "saturation": -100 }
              ]
          }, {
              "featureType": "landscape",
              "stylers": [
                { "saturation": -100 }
              ]
          }, {
              "featureType": "poi",
              "stylers": [
                { "saturation": -100 }
              ]
          }, {
              elementType: 'labels',
              stylers: [
                { hue: '#cc0000' },
                { saturation: 50 },
                { lightness: -10 },
                { gamma: 0.90 }
              ]
          }
        ];


        var mapOptions = {
            zoom: 14,
            center: new google.maps.LatLng(48.9117731, 24.717129),
            mapTypeControlOptions: {
                mapTypeIds: [google.maps.MapTypeId.ROADMAP, MY_MAPTYPE_ID]
            },
            disableDefaultUI: true,
            mapTypeId: MY_MAPTYPE_ID
        };
        map = new google.maps.Map(document.getElementById('map'),
            mapOptions);

        var styledMapOptions = {
            name: 'Custom Style'
        };

        var customMapType = new google.maps.StyledMapType(featureOpts, styledMapOptions);

        map.mapTypes.set(MY_MAPTYPE_ID, customMapType);
    }

}

var index = null;

$(function () {
    index = new Index();
    index.init();
})