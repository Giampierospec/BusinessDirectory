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
                $routeProvider.when("/deleteBusiness/:companyName", {
                    controller: "deleteBusinessController",
                    controllerAs: "vm",
                    templateUrl: "/angularViews/deleteBusiness.html"
                });
                $routeProvider.when("/editBusiness/:companyName", {
                    controller: "editBusinessController",
                    controllerAs: "vm",
                    templateUrl: "/angularViews/editBusiness.html"
                });


                $routeProvider.otherwise({ redirectTo: "/" });
            });
})();