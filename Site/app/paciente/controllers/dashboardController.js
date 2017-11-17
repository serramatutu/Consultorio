'use strict';
app.controller('dashboardController', ['$scope', 'informationService', function ($scope, informationService) {
    $scope.isNavCollapsed = true;
    $scope.isCollapsed = false;
    $scope.isCollapsedHorizontal = true;

    informationService.getAgendaPaciente(function(data) {
        // console.log(data[0]);
        $scope.consultas = data;

        for (let i=0; i<data.length; i++)
          data[i].DataHora = new Date(data[i].DataHora).getHours();

        console.log((new Date(data[0].DataHora)).getHours());
        console.log(data[0].DataHora);
    });
}]);
