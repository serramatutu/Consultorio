'use strict';
app.controller('consultasController', ['$rootScope', '$scope', '$uibModal', 'informationService', 'authService', 'pacienteService', 'utilitiesService', 'modalService',
    function ($rootScope, $scope, $modal, informationService, authService, pacienteService, util, modalService) {
        informationService.getMedicos([]).then(function (response) {
            $scope.medicos = response.data;
        });

        informationService.getEspecialidades().then(function (response) {
            $scope.especialidades = response.data;
        });

        $rootScope.$on('consultaChanged', function () {
            informationService.getAgendaPaciente().then(function (response) {
                for (let i = 0; i < response.data.length; i++)
                    response.data[i].dataHora = new Date(response.data[i].dataHora);

                $scope.consultas = response.data;
            });
        })

        informationService.getAgendaPaciente().then(function (response) {
            for (let i = 0; i < response.data.length; i++)
                response.data[i].dataHora = new Date(response.data[i].dataHora);

            $scope.consultas = response.data;
        });

        $scope.pesquisar = function () {
            return function (consulta) {
                if (!$scope.pesquisa)
                    return true;
                var lowerPesq = $scope.pesquisa.toLowerCase();

                return consulta.medico.nome.toLowerCase().includes(lowerPesq) ||
                    consulta.paciente.nome.toLowerCase().includes(lowerPesq) ||
                    consulta.medico.especialidade.nome.toLowerCase().includes(lowerPesq) ||
                    informationService.getNomeStatusConsulta(consulta.status).toLowerCase().includes(lowerPesq);
            }
        }

        $scope.util = util;
        $scope.getNomeStatusConsulta = informationService.getNomeStatusConsulta;

        $scope.modalConsulta = function (consulta) {
            var modal = $modal.open({
                templateUrl: 'editarconsulta.html',
                controller: 'editarConsultaController',
                size: 'lg',
                resolve: {
                    consulta: function () {
                        return consulta;
                    }
                }
            });
        }

        $scope.agendarConsulta = function (consulta) {

            var modalDefaults = {
                templateUrl: "/app/paciente/modals/agendarModal.html",
                controller: "agendarConsultaController",
                size: 'lg'
            };

            modalService.showModal(modalDefaults);
        }
    }]
);

app.controller('editarConsultaController', ['$rootScope', '$scope', 'utilitiesService', 'informationService', 'pacienteService', 'consulta',
    function ($rootScope, $scope, utilitiesService, informationService, pacienteService, consulta) {
    $scope.consulta = consulta;
    if (!!$scope.consulta.avaliacao)
        $scope.feedback = angular.copy($scope.consulta.avaliacao);
    $scope.getNomeStatusConsulta = informationService.getNomeStatusConsulta;

    // Envia a avaliação do paciente ao servidor
    $scope.avaliarConsulta = function () {
        pacienteService.avaliarConsulta(consulta.id, $scope.feedback).then(function () {
            $rootScope.$emit('consultaChanged', {});
            $scope.$close();
        });
    }

    // Cancela a consulta especificada
    $scope.cancelarConsulta = function () {
        pacienteService.cancelarConsulta(consulta.id).then(function () {
            $rootScope.$emit('consultaChanged', {});
            $scope.$close();
        });
    }

    $scope.util = utilitiesService;
    $scope.close = function () {
        $scope.$close();
    }
}]);
