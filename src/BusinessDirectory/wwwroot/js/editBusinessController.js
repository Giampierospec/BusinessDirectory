//editBusinessController.js
(function () {
    "use strict";
    angular.module("myBusiness-app")
            .controller("editBusinessController", editBusinessController);
    function editBusinessController($routeParams, $http) {
        var vm = this;
        vm.business = {};
        vm.companyName = $routeParams.companyName;
        vm.isBusy = true;
        vm.errorMessage = "";
        var url = "/api/business/" + vm.companyName;
        $http.get(url)
              .then(function (response) {
                  angular.copy(response.data, vm.business);
                  console.log(vm.business);
              }, function (err) {
                  vm.errorMessage = "Ocurrio un error al buscar el negocio "+err;
              })
        .finally(function () {
            vm.isBusy = false;
        });
    
        vm.editBusiness = function () {
            vm.isBusy == true;
            $http.post("/api/business/update", vm.business)
                  .then(function (response) {
                      alert("Negocio actualizado correctamente");
                      window.open("#/", "_self");
                  }, function (err) {
                      vm.errorMessage = "Ocurrio un error al editar el negocio " + err;
                  })
                .finally(function () {
                    vm.isBusy = false;
                });
        }

    }
})();