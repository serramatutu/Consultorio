'use strict';
app.controller('consultasController', ['$scope', 'informationService', 'authService', 'pacienteService', 'utilitiesService',
    function ($scope, informationService, authService, pacienteService, util) {
        informationService.getMedicos([]).then(function (response) {
            $scope.medicos = response.data;
        });

        informationService.getEspecialidades().then(function (response) {
            $scope.especialidades = response.data;
        });

        informationService.getAgendaPaciente().then(function (response) {
            for (let i = 0; i < response.data.length; i++)
                response.data[i].DataHora = new Date(response.data[i].DataHora);

            $scope.consultas = response.data;

            console.log(response.data);
        });

        $scope.util = util;
        $scope.getNomeStatusConsulta = informationService.getNomeStatusConsulta;
        
    }]
);