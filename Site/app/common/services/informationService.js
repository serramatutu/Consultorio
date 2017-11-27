'use strict';
app.factory('informationService', ['$rootScope', '$http', function ($rootScope, $http) {
    var service = {};

    service.getMedicos = function (especialidades, callback) {
        if (!Array.isArray(especialidades))
            especialidades = [especialidades];

        return $http.post($rootScope.apiDomain + '/info/getmedicos', especialidades);
    }

    service.getEspecialidades = function (callback) {
        return $http.get($rootScope.apiDomain + '/info/getespecialidades');
    }

    service.getAgendaPaciente = function(callback){
       return $http.get($rootScope.apiDomain + '/paciente/agenda');
    }

    service.getnomestatusConsulta = function (status) {
        switch (status) {
            case 0: return 'Agendada';
                break;
            case 1: return 'Cancelada';
                break;
            case 2: return 'Realizada';
                break;
        }
    }

    return service;
}]);
