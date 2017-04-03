//myBusiness-app.js
(function () {
    "use strict";
    angular.module("myBusiness-app", ["loadingCircle", "ngRoute"])
            .config(function ($routeProvider) {
                $routeProvider.when("/", {
                    controller: "businessUserController",
                    controllerAs: "vm",
                    templateUrl: "/angularViews/businessUserView.html"
                });
                $routeProvider.when("/detailsUser/:companyName", {
                    controller: "businessUserDetailsController",
                    controllerAs: "vm",
                    templateUrl:"/angularViews/businessDetails.html"
                });

                $routeProvider.otherwise({ redirectTo: "/" });
            });
})();