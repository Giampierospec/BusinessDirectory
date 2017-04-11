//deleteBusinessController.js
(function () {
    "use strict";
    angular.module("publishBusiness-app")
            .controller("deleteBusinessController", deleteBusinessController);
    function deleteBusinessController($http, $routeParams) {
        vm = this;
        vm.companyName = $routeParams.companyName;
    }
})();