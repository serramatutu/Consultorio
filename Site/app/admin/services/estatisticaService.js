'use strict';

app.factory('estatisticaService', ['$rootScope', '$http', function ($rootScope, $http) {
    var serviceFactory = {};

    serviceFactory.getEstatisticaMedico = function () {
        return $http.get($rootScope.apiDomain + '/admin/estatistica/medico');
    }

    serviceFactory.getEstatisticaEspecialidade = function () {
        return $http.get($rootScope.apiDomain + '/admin/estatistica/especialidade');
    }

    serviceFactory.getEstatisticaPaciente = function () {
        return $http.get($rootScope.apiDomain + '/admin/estatistica/paciente');
    }

    serviceFactory.getEstatisticaMesConsulta = function () {
        return $http.get($rootScope.apiDomain + '/admin/estatistica/mesconsulta');
    }

    return serviceFactory;
}]);