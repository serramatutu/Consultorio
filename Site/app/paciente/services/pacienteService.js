'use strict';
app.factory('pacienteService', ['$rootScope', '$http', function ($rootScope, $http) {
    var pacienteServiceFactory = {};

    var _agendarConsulta = function (agendamento) {
        return $http.post($rootScope.apiDomain + '/paciente/agendarconsulta', agendamento).then(function success(response) {
            return response;
        });
    }

    var _avaliarConsulta = function (idConsulta, avaliacao) {
        var data = {
            idConsulta: idConsulta,
            avaliacao: avaliacao
        }
        return $http.post($rootScope.apiDomain + '/paciente/avaliarconsulta', data);
    }

    var _cancelarConsulta = function (idConsulta) {
        return $http.post($rootScope.apiDomain + '/paciente/cancelarconsulta', JSON.stringify(idConsulta));
    }

    pacienteServiceFactory.agendarConsulta = _agendarConsulta;
    pacienteServiceFactory.cancelarConsulta = _cancelarConsulta;
    pacienteServiceFactory.avaliarConsulta = _avaliarConsulta;

    return pacienteServiceFactory;        
}]);