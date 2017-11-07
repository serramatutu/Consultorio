'use strict';
app.controller('indexController', ['$scope', '$rootScope', '$location', 'authService', function ($scope, $rootScope, $location, authService) {
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    console.log($location.path());
    $scope.authService = authService;
    $scope.isCollapsed = true;
}]);
