'use strict';
app.controller('loginController', ['$scope', '$state', 'authService', function ($scope, $state, authService) {
    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        // Quando logou, redireciona para a dashboard
        authService.login($scope.loginData).then(function (response) {
            $state.go("paciente.dashboard");
        },
        function (err) {
            $scope.message = err.data.error_description;
        });
    };
}]);