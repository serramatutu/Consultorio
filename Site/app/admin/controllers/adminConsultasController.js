app.controller('adminConsultasController', ['$scope', 'adminService', 'informationService', function ($scope, adminService, informationService) {
    adminService.getConsultasDePeriodo().then(function (response) {
        $scope.consultas = response.data;
    });

    $scope.diasAtras = function () {
        return function (consulta) {
            //var date = new Date(consulta.dataHora);
            //date.setDate(date.getDate() + $scope.qtdDias);

            //if (date >= Date.now())
            //    return true

            //return false;
            return true;
        };
    }

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

    $scope.getNomeStatusConsulta = informationService.getNomeStatusConsulta();
}]);