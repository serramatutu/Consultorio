'use strict';
app.factory('pacienteService', ['$rootScope', '$http', function ($rootScope, $http) {
    var pacienteServiceFactory = {};

    var _agendarConsulta = function (agendamento) {
        return $http.post($rootScope.apiDomain + '/paciente/agendarconsulta', agendamento).then(function success(response) {
            return response;
        });
    }

    pacienteServiceFactory.agendarConsulta = _agendarConsulta;

    return pacienteServiceFactory;        
}]);