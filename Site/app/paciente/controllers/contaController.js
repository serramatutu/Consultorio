'use strict';
app.controller('contaController', ['$scope', function ($scope) {
    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.conta = {
        userName: "",
        senha: "",
        confSenha: "",
        nomeCompleto: "",
        dataNasc: "",
        email: "",
        telefone: "",
        endereco: ""
    };

    $scope.alterar = function () {
        authService.alterar($scope.conta).then(function (response) {
            $scope.savedSuccessfully = true;
            $scope.message = "Alterado com sucesso!";
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
}]);