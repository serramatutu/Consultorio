'use strict';
app.controller('dashboardController', ['$scope', 'informationService', function ($scope, informationService) {
    $scope.isNavCollapsed = true;
    $scope.isCollapsed = false;
    $scope.isCollapsedHorizontal = true;

    informationService.getAgendaPaciente(function(data) {
        $scope.consultas = data;

        for (let i=0; i<data.length; i++)
          data[i].DataHora = new Date(data[i].DataHora);
    });

    $scope.formatarData = function(data) {
        var meses = [
            "janeiro", "fevereiro", "narço",
            "abril", "maio", "junho", "julho",
            "agosto", "setembro", "outubro",
            "novembro", "dezembro"
        ];

        var dia = data.getDate();
        var indiceMes = data.getMonth();
        var ano = data.getFullYear();
        var hora = data.getHours();
        var min = data.getMinutes();

        return dia + ' de ' + meses[indiceMes] + ' de ' + ano + ' às ' + ("0" + hora).slice(-2) + 'h e ' + ("0" + min).slice(-2) + 'min';
    }
}]);
