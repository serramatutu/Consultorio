app.controller('medicoConsultaController', ['$rootScope', '$scope', 'utilitiesService', 'informationService', 'medicoService', 'consulta',
    function ($rootScope, $scope, utilitiesService, informationService, medicoService, consulta) {
        $scope.finalizacao = {
            idConsulta: consulta.id
        }
        $scope.consulta = consulta;
        $scope.util = utilitiesService;

        // Envia a finalização do médico ao servidor
        $scope.finalizar = function () {
            medicoService.finalizarConsulta($scope.finalizacao).then(function () {
                $rootScope.$emit('consultaChanged', {});
                $scope.$close();
            });
        }

        $scope.close = function () {
            $scope.$close();
        }
    }]);