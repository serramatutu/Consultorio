'use strict';
app.factory('medicoService', ['$rootScope', '$http', function ($rootScope, $http) {
    var medicoServiceFactory = {};

    var _finalizarConsulta = function (finalizacao) {
        return $http.post($rootScope.apiDomain + '/medico/finalizarconsulta', finalizacao);
    }

    var _getConsultaAtual = function () {
        return $http.get($rootScope.apiDomain + '/medico/getconsultaatual');
    }

    medicoServiceFactory.finalizarConsulta = _finalizarConsulta;
    medicoServiceFactory.getConsultaAtual = _getConsultaAtual;

    return medicoServiceFactory;
}]);
