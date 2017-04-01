//loadingCircle.js
(function () {
    "use strict";
    angular.module("loadingCircle", [])
    .directive("waitCursor", waitCursor);
    //This calls the cursor
    function waitCursor() {
        return {
            scope: {
                show: "=displayWhen"
            },
            restrict: "E",
            templateUrl: "/angularViews/waitCursor.html"
        };
    }
})();