'use strict';
app.controller('indexController', ['$scope', '$rootScope', '$location', 'authService', 'access',
    function ($scope, $rootScope, $location, authService, access) {
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.access = access;
    $scope.isCollapsed = true;
}]);
