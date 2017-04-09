//publishBusiness-app.
(function () {
    "use strict";
    angular.module("publishBusiness-app", ["loadingCircle", "ngRoute"])
            .config(function ($routeProvider) {
                $routeProvider.when("/", {
                    controller: "publishBusinessController",
                    controllerAs: "vm",
                    templateUrl: "/angularViews/publishBusinessView.html"

                });
                $routeProvider.when("/detailsPublishBusiness/:companyName", {
                    controller: "publishBusinessDetailsController",
                    controllerAs: "vm",
                    templateUrl: "/angularViews/businessDetails.html"
                });

            });
})();