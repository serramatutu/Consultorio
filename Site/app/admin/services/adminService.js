'use strict';
app.factory('adminService', ['$rootScope', '$http', function ($rootScope, $http) {
    var serviceFactory = {};

    serviceFactory.cadastrarMedico = function (registrationData) {
        return $http.post($rootScope.apiDomain + '/admin/cadastrarmedico', registrationData);
    }

    serviceFactory.cadastrarEspecialidade = function (nomeEspecialidade) {
        return $http.post($rootScope.apiDomain + '/admin/cadastrarespecialidade', '"'+nomeEspecialidade+'"');
    }

    serviceFactory.getConsultasDePeriodo = function (dias) {
        return $http.get($rootScope.apiDomain + '/admin/getconsultasdomes');
    }

    return serviceFactory;
}]);
