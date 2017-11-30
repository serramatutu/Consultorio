'use strict';
app.factory('estatisticaService', ['$rootScope', '$http', function ($rootScope, $http) {
    var serviceFactory = {};

    serviceFactory.getEstatisticaMedico = function () {
        return $http.get($rootScope.apiDomain + '/estatistica/medico');
    }

    return serviceFactory;
}]);