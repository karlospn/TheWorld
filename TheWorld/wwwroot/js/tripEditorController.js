(function() {

    angular.module("app-trips").controller("tripEditorController", tripEditorController);

    function tripEditorController($routeParams, $http) {
        
        var vm = this;

        vm.name = $routeParams.tripName;
        vm.stops = [];
        vm.errorMessage = "";
        vm.isLoading = true;

        $http.get("/api/trips/"+ vm.name +"/stops").then(
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