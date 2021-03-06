﻿'use strict';
app.controller('pacienteDashboardController', ['$scope', 'informationService', 'modalService', function ($scope, informationService, modalService) {
    $scope.isNavCollapsed = true;
    $scope.isCollapsed = false;
    $scope.isCollapsedHorizontal = true;

    informationService.getAgendaPaciente().then(function (response) {
        for (let i = 0; i < response.data.length; i++)
            response.data[i].dataHora = new Date(response.data[i].dataHora);

        $scope.consultas = response.data;
    });

    $scope.agendarConsulta = function (consulta) {
        var modalDefaults = {
            templateUrl: "/app/paciente/modals/agendarModal.html",
            controller: "agendarConsultaController",
            size: 'lg'
        };

        modalService.showModal(modalDefaults);
    }
}]);
