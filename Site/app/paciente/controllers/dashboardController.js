'use strict';
app.controller('dashboardController', ['$scope', 'informationService', function ($scope, informationService) {
    $scope.isNavCollapsed = true;
    $scope.isCollapsed = false;
    $scope.isCollapsedHorizontal = true;

    informationService.getAgendaPaciente().then(function (response) {
        for (let i = 0; i < response.data.length; i++)
            response.data[i].DataHora = new Date(response.data[i].DataHora);

        $scope.consultas = response.data;
    });
}]);
