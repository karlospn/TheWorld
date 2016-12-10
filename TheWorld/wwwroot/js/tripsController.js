/// <reference path="../lib/angular/angular.js" />
//tripController.js

(function () {


    angular.module("app-trips").controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;

        vm.name = "test";
        vm.trips = [];
        vm.newTrip = {};
        vm.errorMessage = "";
        vm.successMessage = "";
        vm.isLoading = true;

        $http.get("/api/trips").then(
            function (response) {
                $.each(response.data,
                    function(index, item) {
                        vm.trips.push({
                            name: item.name,
                            created : item.dateCreated
                        });
                    });
            },
            function() {
                vm.errorMessage = "Error calling the API";
            }).finally(function() {
            vm.isLoading = false;
        });

        vm.AddTrip = function () {

            vm.isLoading = true;
            vm.successMessage = "";
            vm.errorMessage = "";

            $http.post("/api/trips", vm.newTrip)
                .then(function(response) {
                        vm.trips.push(
                        {
                            name: response.data.name,
                            created: response.data.created
                        });

                        vm.successMessage = "Trip added correctly on BBDD";
                    },
                    function() {
                        vm.errorMessage = "Error inserting on BBDD";
                    })
                .finally(function () {
                    vm.newTrip = {};
                    vm.isLoading = false;
                });
        };
    };

})();