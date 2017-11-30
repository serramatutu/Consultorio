'use strict';
app.controller('cadastroMedicoController', ['$scope', 'informationService', 'adminService',
    function ($scope, informationService, adminService) {
    informationService.getEspecialidades().then(function (response) {
        $scope.especialidades = response.data;
    });

    $scope.message = '';
    $scope.savedSuccessfully = false;

    $scope.cadastrar = function () {
        console.log($scope.userData);
        adminService.cadastrarMedico($scope.registration).then(function (response) {
            $scope.message = "Cadastrado com sucesso!";
            $scope.savedSuccessfully = true;
        }, function (response) {
            $scope.savedSuccessfully = false;
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

    $scope.cancel = function () {
        $scope.$close();
    }
}]);
