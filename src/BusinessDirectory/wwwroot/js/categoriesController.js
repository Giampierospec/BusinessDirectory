//categoriesController.js

(function () {
    "use strict";
    angular.module("app-category")
            .controller("categoriesController", categoriesController);
    function categoriesController($http) {
       var  vm = this;
        vm.category = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        $http.get("/api/category")
              .then(function (response) {                  
                  angular.copy(response.data, vm.category);
                  $("body").css({ "background": "url('../images/4291.jpg')", "background-size": "cover" });
              },
              function (error) {
                  vm.errorMessage = "Ocurrio un error al buscar las categorias " + error;
              })
                .finally(function () {
                    vm.isBusy = false;
                });
    }
})();