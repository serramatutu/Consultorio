'use strict';
app.controller('cadastroMedicoController', ['$scope', 'informationService', 'adminService',
    function ($scope, informationService, adminService) {
    informationService.getEspecialidades().then(function (response) {
        $scope.especialidades = response.data;
    });

    $scope.loginData = {};
    $scope.userData = {};

    $scope.cadastrar = function () {
        console.log($scope.userData);
        adminService.cadastrarMedico($scope.loginData, $scope.userData).then(function (response) {
            console.log(response.data);
        });
    };
}]);
