app.controller('medicoController', ['$rootScope', '$scope', 'informationService', 'medicoService', 'modalService', 'authService',
    function ($rootScope, $scope, informationService, medicoService, modalService, authService) {
    $scope.logOut = authService.logOut;

    function getConsulta() {
        medicoService.getConsultaAtual().then(function (response) {
            $scope.consultaAtual = response.data;
            $scope.consultaAtual.dataHora = new Date($scope.consultaAtual.dataHora);
        });
    }
    getConsulta();    
    $rootScope.$on('consultaChanged', getConsulta)

    $scope.editarConsultaAtual = function () {
        var modalDefaults = {
            templateUrl: "/app/medico/modals/editarConsultaAtual.html",
            controller: "medicoConsultaController",
            size: 'lg',
            resolve: {
                consulta: function () {
                    return $scope.consultaAtual;
                }
            }
        };

        var modal = modalService.showModal(modalDefaults);
    }
}]);