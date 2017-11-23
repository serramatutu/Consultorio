'use strict';
app.controller('consultasController', ['$scope', '$uibModal', 'informationService', 'authService', 'pacienteService', 'utilitiesService',
    function ($scope, $modal, informationService, authService, pacienteService, util) {
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

app.controller('editarConsultaModalController', ['$scope', 'utilitiesService', 'informationService', 'pacienteService', 'consulta',
    function ($scope, utilitiesService, informationService, pacienteService, consulta) {
    $scope.consulta = consulta;
    $scope.getNomeStatusConsulta = informationService.getNomeStatusConsulta;

    var avaliacao = informationService.getNomeStatusConsulta(consulta) == 'Realizada' ? {} : undefined;

    // Envia a avaliação do paciente ao servidor
    $scope.avaliar = function () {
        pacienteService.avaliarConsulta(consulta.Id, avaliacao).then(function () {
            $scope.$close();
        })
    }

    // Cancela a consulta especificada
    $scope.cancelarConsulta = function () {
        pacienteService.cancelarConsulta(consulta.Id).then(function () {
            $scope.$close();
        })
    }

    $scope.util = utilitiesService;
    $scope.close = function () {
        $scope.$close();
    }
}]);