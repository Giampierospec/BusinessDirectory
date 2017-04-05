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

            });
})();