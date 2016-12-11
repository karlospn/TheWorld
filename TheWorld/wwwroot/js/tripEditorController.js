(function() {

    angular.module("app-trips").controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        
        var vm = this;

        vm.name = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.successMessage = "";
        vm.isLoading = true;
        vm.newStop = {};

        var url = "/api/trips/" + vm.name + "/stops";

        $http.get(url).then(
           function (response) {
               $.each(response.data,
                   function (index, item) {
                       vm.stops.push({
                           name: item.name,
                           arrival: item.arrival,
                           latitude: item.latitude,
                           longitude: item.longitude
                       });
                   });
               _showMap(vm.stops);
           },
           function () {
               vm.errorMessage = "Error calling the API";
           }).finally(function () {
               vm.isLoading = false;
           });


        vm.addStop = function () {

            vm.isLoading = true;
            vm.successMessage = "";
            
            $http.post(url, vm.newStop)
                .then(function (response) {
                        vm.stops.push({
                            name: response.data.name,
                            arrival: response.data.arrival,
                            latitude: response.data.latitude,
                            longitude: response.data.longitude

                        });
                        vm.successMessage = "Trip added correctly on BBDD";
                        _showMap(vm.stops);
                    },
                    function () {
                        vm.errorMessage = "Error inserting on BBDD";
                    })
                .finally(function () {
                    vm.newStop = {};
                    vm.isLoading = false;
                });
        }
    }


    function _showMap(stops) {

        var mapStops = _.map(stops,
            function (item) {
                return{
                    info : item.name,
                    lat: item.latitude,
                    long: item.longitude                    
                }
            });

        if (stops && stops.length > 0) {
            travelMap.createMap({
                stops: mapStops,
                selector: "#map"
            });
        }

    }


})();