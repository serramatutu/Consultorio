'use strict';
app.controller('cadastroMedicoController', ['$scope', 'informationService', function ($scope, informationService) {
    informationService.getEspecialidades().then(function (response) {
        $scope.especialidades = response.data;
    });

    $scope.cadastrar = function(){
        console.log('cadastrar');
    };
}]);
