//editBusinessController.js
(function () {
    "use strict";
    angular.module("publishBusiness-app")
            .controller("editBusinessController", editBusinessController);
    function editBusinessController($routeParams, $http) {
        var vm = this;
        vm.Business = {};
        vm.isBusy = true;
        vm.errorMessage = "";


    }
})();