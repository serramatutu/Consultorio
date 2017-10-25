'use strict';
app.controller('cadastroController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {
    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };

    $scope.cadastrar = function () {
        authService.cadastrar($scope.registration).then(function (response) {
            $scope.savedSuccessfully = true;
            $scope.message = "Cadastrado com sucesso! Redirecionando para login...";
            loginTimer();
        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Não pôde cadastrar o usuário devido a: " + errors.join(' ');
         });
    };

    var loginTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }
}]);