//businessControllerFull.js

(function () {
    "use strict";
    //Getting the existing module
    angular.module("business-module")
            .controller("businessControllerFull", businessControllerFull);

    function businessControllerFull($http) {
        var vm = this;
        vm.businesses = [];
        vm.errorMessage = "";
        vm.isBusy = true;
        $http.get("/api/business")
        .then(function (response) {
            //If it succeeds
            angular.copy(response.data, vm.businesses);
            initMap(vm.businesses);

        }, function (error) {
            vm.errorMessage = "Fallo en la obtencion de negocios " + error;
        })
        .finally(function () {
            vm.isBusy = false;
        });
    }

    function initMap(businesses) {
        //Creation of a new instance of GMaps
        var map = new GMaps({
            div: "#map",
            lat: 0,
            lng: 0,
            zoom: 8,
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
                },
               click: function (e) {
                    map.setCenter(Number(bs.latitude), Number(bs.longitude))
                    map.setZoom(16);
                }
            });
        }
    }

})();