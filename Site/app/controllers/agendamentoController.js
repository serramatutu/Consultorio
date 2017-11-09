'use strict';
app.controller('agendamentoConroller', ['$scope', 'authService', function ($scope, authService) {
    $scope.savedSuccessfully = false;
    $scope.agendamento = {
        dataHora: '',
        duracao: 0,
        crmMedicoResponsavel: ''
    };

    $scope.agendar = function() {

    }
});