'use strict';
app.controller('medicoDashboardController', ['$scope', 'informationService',
    function ($scope, informationService) {
    informationService.getAgendaMedico().then(function (response) {
        for (let i = 0; i < response.data.length; i++)
            response.data[i].dataHora = new Date(response.data[i].dataHora);

        $scope.consultas = response.data;
    });
}]);
