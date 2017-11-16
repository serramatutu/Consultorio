'use strict';
app.controller('indexController', ['$scope', '$rootScope', '$location', 'authService', function ($scope, $rootScope, $location, authService) {
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.authService = authService;
    $scope.isCollapsed = true;
}]);
