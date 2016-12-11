//simpleControls.js

(function () {

    angular.module("simpleControls", [])
        .directive("waiting", waiting);

    function waiting() {
        return {
            scope: {
              render : "=displaywhen"  
            },
            templateUrl : "/views/waitingView.html"
        };
    }


})();