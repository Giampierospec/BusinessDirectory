//publishBusinessDetailsController.js
(function () {
    angular.module("publishBusiness-app")
            .controller("publishBusinessDetailsController", publishBusinessDetailsController);

    function publishBusinessDetailsController($routeParams, $http) {
        var vm = this;
        vm.companyName = $routeParams.companyName;
        console.log(vm.companyName);
        vm.business = {};
        var url = "/api/business/" + vm.companyName;
        vm.errorMessage = "";
        vm.isBusy = true;
        console.log(url);
        $http.get(url)
        .then(function (response) {
            //success
            angular.copy(response.data, vm.business);
            _showTheMap(vm.business);
        }, function (error) {
            //failure
            vm.errorMessage = "Fallo en la obtencion del negocio " + error;
        }).finally(function () {
            vm.isBusy = false;
        });
    }
    function _showTheMap(business) {
        var map = new GMaps({
            div: "#map",
            lat: 0,
            lng: 0,
            zoom: 7,
            zoomControl: true
        });
        GMaps.geocode({
            address: business.address,
            callback: function (results, status) {
                if (status == "OK") {
                    var latlng = results[0].geometry.location;
                    map.setCenter(latlng.lat(), latlng.lng());
                    map.addMarker({
                        lat: latlng.lat(),
                        lng: latlng.lng(),
                        title: business.companyName,
                        infoWindow: {
                            content: "<p>" + business.companyName + "</p>"
                        },
                        click: function (e) {
                            map.setCenter(latlng.lat(), latlng.lng());
                            map.setZoom(16);
                        }
                    });

                }

            }
        });
    }
        
})();