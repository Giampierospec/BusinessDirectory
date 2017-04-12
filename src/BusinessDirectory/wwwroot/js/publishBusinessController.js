//publishBusinessController.js

(function () {
    "use strict";
    angular.module("publishBusiness-app")
            .controller("publishBusinessController", publishBusinessController);
    function publishBusinessController($http) {
        var vm = this;
        vm.categories = [];
        vm.businesses = [];
        vm.categoryName = {};
        vm.business = {};
        vm.errorMessage = "";
        vm.isBusy = true;
        //Gets the categories
        $http.get("/api/category")
              .then(function (response) {
                  //success
                  angular.copy(response.data, vm.categories);
              }, function (error) {
                  vm.errorMessage = "No se pudo obtener las categorias " + error;
              })
                .finally(function () {
                    vm.isBusy = false;
                });
        //gets the business by userName
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
        
        //This will add my business
        vm.addBusiness = function () {
            var categoryName = vm.category;
            //this will get the name of my category
            var url = "/api/category/" + categoryName.name.name + "/business";
            vm.isBusy = true;
            $http.post(url, vm.business)
            .then(function (response) {
                // success
                vm.businesses.push(response.data);
                _showThisMap(vm.businesses);
                vm.business = {};
                vm.category = {};
            }, function (err) {
                // failure
                vm.errorMessage = "Fallo en agregar nuevo negocio";
            })
            .finally(function () {
                vm.isBusy = false;
            });
        };

        function _showThisMap(businesses) {
            //Creation of a new instance of GMaps
            var map = new GMaps({
                div: "#map",
                lat: 0,
                lng: 0,
                zoom: 10,
                zoomControl: true
            });
            //This will get my actual position
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
            //This will add some markers
                map.addMarker({
                    lat: Number(bs.latitude),
                    lng: Number(bs.longitude),
                    title: bs.companyName,
                    infoWindow: {
                        content: "<p>" + bs.companyName + "</p>"
                    },
                    click: function (e) {
                        map.setCenter(Number(bs.latitude), Number(bs.longitude));
                        map.setZoom(16);
                    }
                });
            }
        }
        
    }
})();