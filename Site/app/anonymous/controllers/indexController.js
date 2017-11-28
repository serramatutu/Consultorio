'use strict';
app.controller('indexController', ['$scope', '$rootScope', '$location', 'authService', 'userState',
    function ($scope, $rootScope, $location, authService, userState) {
    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    userState.then(function (userData) {
        $scope.userData = userData;
    });
    $scope.isCollapsed = true;
}]);
