'use strict';
app.controller('cadastroMedicoController', ['$scope', 'informationService', 'adminService',
    function ($scope, informationService, adminService) {
    informationService.getEspecialidades().then(function (response) {
        $scope.especialidades = response.data;
    });

    $scope.message = '';

    $scope.cadastrar = function () {
        console.log($scope.userData);
        adminService.cadastrarMedico($scope.registration).then(function (response) {
            $scope.message = "Cadastrado com sucesso!";
            $rootScope.$emit('medicoChanged', {});
            $scope.$close();
        }, function (response) {
            var errors = [];
            var ms = response.data.modelState;
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
