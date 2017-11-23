'use strict';
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

        $scope.agendarConsulta = function (consulta) {
            var modal = $modal.open({
                templateUrl: 'agendarconsulta.html',
                controller: 'agendarConsultaModalController',
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
            $rootScope.$emit('consultaChanged', {});
            $scope.$close();
        });
    }

    // Cancela a consulta especificada
    $scope.cancelarConsulta = function () {
        pacienteService.cancelarConsulta(consulta.Id).then(function () {
            $rootScope.$emit('consultaChanged', {});
            $scope.$close();
        });
    }

    $scope.util = utilitiesService;
    $scope.close = function () {
        $scope.$close();
    }
}]);

app.controller('agendarConsultaModalController', ['$rootScope', '$scope', 'informationService', 'authService', 'pacienteService', 'consulta',
    function ($rootScope, $scope, informationService, authService, pacienteService, consulta) {
        informationService.getMedicos([]).then(function (response) {
            $scope.medicos = response.data;
        });

        informationService.getEspecialidades().then(function (response) {
            $scope.especialidades = response.data;
        });

        $scope.message = '';

        $scope.consulta = consulta;

        $scope.agendar = function () {
            var data = {
                dataHora: $scope.consulta.dataHora,
                duracao: $scope.consulta.duracao,
                crmMedicoResponsavel: $scope.consulta.crmMedicoResponsavel.CRM
            };

            pacienteService.agendarConsulta(data).then(function success(response) {
                $rootScope.$emit('consultaChanged', {});
                $scope.$close();
            }, function err(response) {
                var errors = [];
                var ms = response.data.ModelState || response.data.modelState;
                if (!!ms)
                    for (var key in ms) {
                        for (var i = 0; i < ms[key].length; i++) {
                            errors.push(ms[key][i]);
                        }
                    }
                else
                    errors.push(response.data.Message);

                $scope.message = "Não pôde agendar a consulta devido a: " + errors.join(' ');
            });
        }

        $scope.cancel = function () {
            $scope.$close();
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