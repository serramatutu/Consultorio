'use strict';
app.controller('agendamentoController', ['$scope', 'informationService', 'authService', 'pacienteService',
    function ($scope, informationService, authService, pacienteService) {
    informationService.getMedicos([]).then(function (response) {
        $scope.medicos = response.data;
    });

    informationService.getEspecialidades().then(function (response) {
        $scope.especialidades = response.data;
    });

    $scope.savedSuccessfully = false;
    $scope.consulta = {
        dataHora: '',
        duracao: 0,
        crmMedicoResponsavel: ''
    };

    $scope.agendar = function () {
        $scope.consulta.crmMedicoResponsavel = $scope.consulta.Medico.CRM; // TODO: Desfazer essa gambiarra
        pacienteService.agendarConsulta($scope.consulta).then(function success(response) {
            $scope.savedSuccessfully = true;
            $scope.message = 'Agendado com sucesso!';
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
    
    $scope.$on('$viewContentLoaded', function () { // Data mínima é hoje
        var date = new Date();
        date.setMinutes(date.getMinutes() - date.getTimezoneOffset());
        date.setSeconds(0);
        date.setMilliseconds(0);

        var min = date.toJSON().slice(0, 19);
        document.getElementById('dataConsulta').setAttribute('min', min);
    });
}]);