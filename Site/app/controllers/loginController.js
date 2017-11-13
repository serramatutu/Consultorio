'use strict';
app.controller('loginController', ['$scope', '$window', 'authService', function ($scope, $window, authService) {
    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        // Quando logou, redireciona para a dashboard
        authService.login($scope.loginData).then(function (response) {
            $window.location.href = '/dashboard.html';
        },
        function (err) {
            $scope.message = err.error_description;
        });
    };
}]);