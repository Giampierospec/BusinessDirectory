//categoriesDetailsController

(function () {
    "use strict";
    angular.module("app-category")
            .controller("categoriesDetailsController", categoriesDetailsController);
    function categoriesDetailsController($routeParams, $http) {
        var vm = this;
        vm.businesses = [];
        vm.categoryName = $routeParams.categoryName;
        vm.errorMessage = "";
        vm.isBusy = true;
        var url = "/api/category/"+vm.categoryName+"/business"
        $http.get(url)
             .then(function (response) {
                 angular.copy(response.data, vm.businesses);
                 $("body").css('background', 'url("")');
                 _showThisMap(vm.businesses);
             }, function (err) {
                 vm.errorMessage = "No se pudieron obtener los negocios relacionados a esta categoria por: " + err;
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