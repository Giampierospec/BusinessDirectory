//deleteBusinessController.js
(function () {
    "use strict";
    angular.module("myBusiness-app")
            .controller("deleteBusinessController", deleteBusinessController);
    function deleteBusinessController($http, $routeParams) {
        var vm = this;
        vm.companyName = $routeParams.companyName;
        vm.business = {};
        vm.errorMessage = "";
        var url = "/api/business/"+vm.companyName;
        vm.isBusy = true;
        $http.get(url)
                .then(function (response) {
                    angular.copy(response.data, vm.business);
                    console.log(vm.business)
                }, function (err) {
                    vm.errorMessage = "No se pudo obtener el negocio por este error " + err;
                })
                .finally(function () {
                    vm.isBusy = false;
                });
        vm.deleteBusiness = function () {
            vm.isBusy = true;
            console.log(vm.business);
            $http.post("/api/business/delete",vm.business)
                   .then(function (response) {
                       alert("Negocio eliminado satisfactoriamente");
                       window.open("#/", "_self");
                       vm.business = {};
                   }, function (err) {
                       vm.errorMessage = "Hubo un error al eliminar el negocio este no existe." + err;
                       console.log(err);
                   })
                    .finally(function () {
                        vm.isBusy = false;
                    });
        }
        

    }
})();