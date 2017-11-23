﻿'use strict';
app.controller('consultasController', ['$rootScope', '$scope', '$uibModal', 'informationService', 'authService', 'pacienteService', 'utilitiesService',
    function ($rootScope, $scope, $modal, informationService, authService, pacienteService, util) {
        informationService.getMedicos([]).then(function (response) {
            $scope.medicos = response.data;
        });

        informationService.getEspecialidades().then(function (response) {
            $scope.especialidades = response.data;
        });

        $rootScope.$on('consultaChanged', function () {
            informationService.getAgendaPaciente().then(function (response) {
                for (let i = 0; i < response.data.length; i++)
                    response.data[i].DataHora = new Date(response.data[i].DataHora);

                $scope.consultas = response.data;
            });
        })

        informationService.getAgendaPaciente().then(function (response) {
            for (let i = 0; i < response.data.length; i++)
                response.data[i].DataHora = new Date(response.data[i].DataHora);

            $scope.consultas = response.data;
        });

        $scope.util = util;
        $scope.getNomeStatusConsulta = informationService.getNomeStatusConsulta;

        $scope.modalConsulta = function (consulta) {
            var modal = $modal.open({
                templateUrl: 'editarconsulta.html',
                controller: 'editarConsultaModalController',
                resolve: {
                    consulta: function () {
                        return consulta;
                    }
                }
            });
        }
    }]
);

app.controller('editarConsultaModalController', ['$rootScope', '$scope', 'utilitiesService', 'informationService', 'pacienteService', 'consulta',
    function ($rootScope, $scope, utilitiesService, informationService, pacienteService, consulta) {
    $scope.consulta = consulta;
    $scope.getNomeStatusConsulta = informationService.getNomeStatusConsulta;

    // Envia a avaliação do paciente ao servidor
    $scope.avaliarConsulta = function () {
        pacienteService.avaliarConsulta(consulta.Id, $scope.consulta.Avaliacao).then(function () {
            $scope.$close();
            $rootScope.$emit('consultaChanged', {});
        });
    }

    // Cancela a consulta especificada
    $scope.cancelarConsulta = function () {
        pacienteService.cancelarConsulta(consulta.Id).then(function () {
            $scope.$close();
            $rootScope.$emit('consultaChanged', {});
        });
    }

    $scope.util = utilitiesService;
    $scope.close = function () {
        $scope.$close();
    }
}]);