app.controller('agendarConsultaController', ['$rootScope', '$scope', 'informationService', 'authService', 'pacienteService',
    function ($rootScope, $scope, informationService, authService, pacienteService) {
        informationService.getMedicos([]).then(function (response) {
            $scope.medicos = response.data;
        });

        informationService.getEspecialidades().then(function (response) {
            $scope.especialidades = response.data;
        });

        $scope.message = '';

        $scope.consulta = {};

        $scope.agendar = function () {
            var data = {
                dataHora: $scope.consulta.dataHora,
                duracao: $scope.consulta.duracao,
                crmMedicoResponsavel: $scope.consulta.crmMedicoResponsavel.crm
            };

            pacienteService.agendarConsulta(data).then(function success(response) {
                $rootScope.$emit('consultaChanged', {});
                $scope.$close();
            }, function err(response) {
                var errors = [];
                var ms = response.data.modelState;
                if (!!ms)
                    for (var key in ms) {
                        for (var i = 0; i < ms[key].length; i++) {
                            errors.push(ms[key][i]);
                        }
                    }
                else
                    errors.push(response.data.message);

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
