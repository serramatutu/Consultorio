'use strict';
app.controller('cadastroEspecialidadeController', ['$scope', 'adminService',
    function ($scope, adminService) {
    $scope.message = '';

    $scope.cadastrar = function () {
        adminService.cadastrarEspecialidade($scope.nomeEspecialidade).then(function (response) {
            $scope.message = "Cadastrado com sucesso!";
            $rootScope.$emit('especialidadeChanged', {});
            $scope.$close();
        }, function (response) {
            $scope.message = response.data.message;
        });
    };

    $scope.cancel = function () {
        $scope.$close();
    }
}]);
