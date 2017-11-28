'use strict';
app.controller('contaController', ['$scope', '$uibModal', 'authService', function ($scope, $modal, authService) {

    $scope.open = function (modal) {
        switch (modal) {
            case 'nome':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'nome.html',
                    controller: 'contaController'
                });
                break;
            case 'email':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'email.html',
                    controller: 'contaController'
                });
                break;
            case 'telefone':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'telefone.html',
                    controller: 'contaController'
                });
                break;
            case 'celular':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'celular.html',
                    controller: 'contaController'
                });
                break;
            case 'endereco':
                $scope.modalInstance = $modal.open({
                    ariaLabelledBy: 'modal-title',
                    ariaDescribedBy: 'modal-body',
                    templateUrl: 'endereco.html',
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
                    templateUrl: 'userName.html',
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

    // Obtém as informações do usuário atual
    authService.getUserData().then(function (response) {
        $scope.loginData = response.data.loginData;
        $scope.userData = response.data.paciente;
    });

    $scope.savedSuccessfully = false;
    $scope.message = "";

    //$scope.alterar = function (campo) {
    //    authService.alterar($scope).then(function (response) {
    //        $scope.savedSuccessfully = true;
    //        $scope.message = "Alterado com sucesso!";
    //    },
    //        function (response) {
    //            var errors = [];
    //            var ms = response.data.ModelState || response.data.modelState;
    //            for (var key in ms) {
    //                for (var i = 0; i < ms[key].length; i++) {
    //                    errors.push(ms[key][i]);
    //                }
    //            }
    //            $scope.message = "Não pôde cadastrar o usuário devido a: " + errors.join(' ');
    //        });
    //};
}]);
