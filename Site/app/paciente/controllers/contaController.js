'use strict';
app.controller('contaController', ['$scope', '$uibModal', function ($scope, $modal) {

    $scope.open = function (modal) {
        switch (modal) {
            case 'nome':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'Nome.html',
                    controller: 'contaController'
                });
                break;
            case 'email':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'Email.html',
                    controller: 'contaController'
                });
                break;
            case 'telefone':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'Telefone.html',
                    controller: 'contaController'
                });
                break;
            case 'celular':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'Celular.html',
                    controller: 'contaController'
                });
                break;
            case 'endereco':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'Endereco.html',
                    controller: 'contaController'
                });
                break;
            case 'senha':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'Senha.html',
                    controller: 'contaController'
                });
                break;
            case 'username':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'UserName.html',
                    controller: 'contaController'
                });
                break;
        }
    }

    $scope.cancel = function(){
        $scope.$close();
    }

    $scope.ok = function(campo){
        switch (campo) {
            case 'nome':
                console.log(document.getElementsByName('value')[0].value);
                break;
            case 'email':
                console.log(document.getElementsByName('value')[0].value);
                break;
            case 'telefone':
                console.log(document.getElementsByName('value')[0].value);
                break;
            case 'celular':
                console.log(document.getElementsByName('value')[0].value);
                break;
            case 'endereco':
                console.log(document.getElementsByName('value')[0].value);
                break;
            case 'senha':
                console.log(document.getElementsByName('value')[0].value);
                console.log(document.getElementsByName('confValue')[0].value);
                break;
            case 'username':
                console.log(document.getElementsByName('value')[0].value);
        }

    }

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
