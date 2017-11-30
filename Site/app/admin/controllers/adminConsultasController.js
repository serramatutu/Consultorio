app.controller('adminConsultasController', ['$scope', 'adminService', 'informationService', function ($scope, adminService, informationService) {
    adminService.getConsultasDePeriodo().then(function (response) {
        $scope.consultas = response.data;
    });

    $scope.diasAtras = function () {
        return function (consulta, dias) {
            var date = new Date(consulta.dataHora);
            date.setDate(date.getDate() + dias);

            if (date >= Date.now())
                return true

            return false;
        };
    }

    $scope.getNomeStatusConsulta = informationService.getNomeStatusConsulta();
}]);