'use strict';
app.controller('agendamentoController', ['$scope', 'informationService', 'authService', function ($scope, informationService, authService) {
    informationService.getMedicos([], function (data) {
        $scope.medicos = data;
    });

    $scope.savedSuccessfully = false;
    $scope.agendamento = {
        dataHora: '',
        duracao: 0,
        crmMedicoResponsavel: ''
    };

    $scope.agendar = function() {

    }
    
    $scope.$on('$viewContentLoaded', function () { // Data mínima é hoje
        var date = new Date();
        date.setMinutes(date.getMinutes() - date.getTimezoneOffset());
        date.setSeconds(0);
        date.setMilliseconds(0);

        var min = date.toJSON().slice(0, 19);
        document.getElementById('dataConsulta').setAttribute('min', min);
    });
}]);