//business-module.js

(function () {
    "use strict";
    angular.module("business-module", ["loadingCircle", "ngRoute"])
    .config(function ($routeProvider) {
        $routeProvider.when("/", {
            controller: "businessControllerFull",
            controllerAs: "vm",
            templateUrl: "/angularViews/BusinessesView.html"
        });
        $routeProvider.when("/details/:companyName", {
            controller: "businessDetailsController",
            controllerAs: "vm",
            templateUrl:"/angularViews/businessDetails.html"

        });
        $routeProvider.otherwise({ redirectTo: "/" });
    });
})();