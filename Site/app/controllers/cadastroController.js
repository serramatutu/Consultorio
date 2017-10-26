﻿'use strict';
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
            var ms = response.data.ModelState || response.data.modelState;
            for (var key in ms) {
                for (var i = 0; i < ms[key].length; i++) {
                    errors.push(ms[key][i]);
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