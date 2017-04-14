//businessDetailsController.js
(function () {
    angular.module("business-module")
            .controller("businessDetailsController", businessDetailsController);

    function businessDetailsController($routeParams, $http) {
        var vm = this;
         vm.companyName = $routeParams.companyName;
        vm.business = {};
        var url = "/api/business/" + vm.companyName;
        vm.errorMessage = "";
        vm.isBusy = true;
        console.log(url);
        $http.get(url)
        .then(function (response) {
            //success
            angular.copy(response.data, vm.business);
            _showTheMap(vm.business);
        }, function (error) {
            //failure
            vm.errorMessage = "Fallo en la obtencion del negocio " + error;
        }).finally(function () {
            vm.isBusy = false;
        });
    }
    function _showTheMap(business) {
        var map = new GMaps({
            div: "#map",
            lat: 0,
            lng: 0,
            zoom: 7,
            zoomControl: true
        });    
        GMaps.geocode({
            address: business.address,
            callback: function (results, status) {
                if (status == "OK") {
                    var latlng = results[0].geometry.location;
                    map.setCenter(latlng.lat(), latlng.lng());
                    map.addMarker({
                        lat: latlng.lat(),
                        lng: latlng.lng(),
                        title: business.companyName,
                        infoWindow: {
                            content: "<p>" + business.companyName + "</p>"
                        },
                        click: function (e) {
                            map.setCenter(latlng.lat(), latlng.lng());
                            map.setZoom(16);
                        }
                    });
                    
                }
            }
        });
        /*GMaps.geolocate({
            success: function (position) {
                map.setCenter(position.coords.latitude, position.coords.longitude);
            },
            error: function(error) {
                alert('Geolocalizacion fallo por este error: '+error.message);
            },
            not_supported: function() {
                alert("Tu navegador no soporta geolocalizacion");
            },
            always: function() {
                console.log("Listo!");
            }
        });
        map.addMarker({
            lat: Number(business.latitude),
            lng: Number(business.longitude),
            title: business.companyName,
            infoWindow: {
                content: "<p>" + business.companyName + "</p>"
            },
            click: function (e) {
                map.setCenter(Number(business.latitude), Number(business.longitude));
                map.setZoom(16);
            }
        });*/
    }
})();