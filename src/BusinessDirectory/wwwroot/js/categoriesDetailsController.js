//categoriesDetailsController

(function () {
    "use strict";
    angular.module("app-category")
            .controller("categoriesDetailsController", categoriesDetailsController);
    function categoriesDetailsController($routeParams, $http) {
        var vm = this;
        vm.businesses = [];
        vm.categoryName = $routeParams.categoryName;
        vm.errorMessage = "";
        vm.isBusy = true;
        var url = "/api/category/"+vm.categoryName+"/business"
        $http.get(url)
             .then(function (response) {
                 angular.copy(response.data, vm.businesses);
                 $("body").css('background', 'url("")');
                 _showThisMap(vm.businesses);
             }, function (err) {
                 vm.errorMessage = "No se pudieron obtener los negocios relacionados a esta categoria por: " + err;
             })
            .finally(function () {
                vm.isBusy = false;
            });
    }
    function _showThisMap(businesses) {
        //Creation of a new instance of GMaps
        var map = new GMaps({
            div: "#map",
            lat: 0,
            lng: 0,
            zoom: 10,
            zoomControl: true
        });
        //This will get my actual position
        GMaps.geolocate({
            success: function (position) {
                map.setCenter(position.coords.latitude, position.coords.longitude);
            },
            error: function (error) {
                alert('Geolocalizacion fallo por este error: ' + error.message);
            },
            not_supported: function () {
                alert("Tu navegador no soporta geolocalizacion");
            },
            always: function () {
                console.log("Listo!");
            }
        });
        for (var bs of businesses) {
        //This will add some markers
            map.addMarker({
                lat: Number(bs.latitude),
                lng: Number(bs.longitude),
                title: bs.companyName,
                infoWindow: {
                    content: "<p>" + bs.companyName + "</p>"
                }
            });
        }
    }
})();