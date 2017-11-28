'use strict';
app.controller('cadastroMedicoController', ['$scope', 'informationService', function ($scope, informationService) {
    informationService.getEspecialidades().then(function (response) {
        $scope.especialidades = response.data;
    });

    $scope.loginData = {};
    $scope.userData = {};

    $scope.cadastrar = function(){
        var data = {
            userModel: $scope.loginData,
            medico: $scope.userData
        };

        console.log(data);
    };
}]);
