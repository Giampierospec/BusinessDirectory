//businessUserController.js
(function () {
    "use strict";
    angular.module("myBusiness-app")
            .controller("businessUserController", businessUserController);
    function businessUserController($http) {
        var vm = this;
        vm.businesses = [];
        vm.isBusy = true;
        vm.businessUser = {};
        vm.errorMessage = "";
        //This will get the user
        $http.get("/api/businessUserByName")
                .then(function (response) {
                    angular.copy(response.data, vm.businessUser);
                }, function (error) {
                    vm.errorMessage = "Ocurrío un error al buscar el usuario";
                });
        $http.get("/api/business/userName")
              .then(function (response) {
                  if (response.data.length == 0) {
                      vm.errorMessage = "Este usuario no tiene negocios registrados";
                  }
                  else {
                      console.log(response.data);
                      angular.copy(response.data, vm.businesses);
                      _showThisMap(vm.businesses);
                  }
                  

              }, function (err) {
                  vm.errorMessage = "no se pudo obtener los negocios por usuario " + err;
              })
              .finally(function () {
                  vm.isBusy = false;
              });
    }
    function _showThisMap(businesses) {
        //Creation of a new instance of GMaps
        var map = new GMaps({
            div: "#map",
            lat: 0,
            lng: 0,
            zoom: 10,
            zoomControl: true
        });
        GMaps.geolocate({
            success: function (position) {
                map.setCenter(position.coords.latitude, position.coords.longitude);
            },
            error: function (error) {
                alert('Geolocalizacion fallo por este error: ' + error.message);
            },
            not_supported: function () {
                alert("Tu navegador no soporta geolocalizacion");
            },
            always: function () {
                console.log("Listo!");
            }
        });
        for (var bs of businesses) {
            GMaps.geocode({
                address: bs.address,
                callback: function (results, status) {
                    if (status == "OK") {

                        var latlng = results[0].geometry.location;
                        var addr = results[0].formatted_address;
                        map.addMarker({
                            lat: latlng.lat(),
                            lng: latlng.lng(),
                            title: addr,
                            infoWindow: {
                                content: "<p>" + addr + "</p>"
                            },
                            click: function (e) {
                                map.setCenter(latlng.lat(), latlng.lng());
                                map.setZoom(16);
                            }
                        });

                    }
                }
            });

        }
    }
})();