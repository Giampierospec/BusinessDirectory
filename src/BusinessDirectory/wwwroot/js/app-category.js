//app-category.js
(function () {
    "use strict";
    angular.module("app-category", ["loadingCircle", "ngRoute"])
            .config(function ($routeProvider) {
                $routeProvider.when("/",{
                    controller: "categoriesController",
                    controllerAs: "vm",
                    templateUrl: "/angularViews/categoriesView.html"
                });
                $routeProvider.when("/details/:categoryName", {
                    controller: "categoriesDetailsController",
                    controllerAs: "vm",
                    templateUrl:"/angularViews/categoriesDetails.html"
                });
                $routeProvider.when("/detailsBusiness/:companyName", {
                    controller: "businessCategoryDetailController",
                    controllerAs: "vm",
                    templateUrl:"/angularViews/businessDetails.html"
                });
                $routeProvider.otherwise({ redirectTo: "/" });
            });
})();