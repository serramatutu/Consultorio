'use strict';
app.factory('informationService', ['$rootScope', '$http', function ($rootScope, $http) {
    var service = {};

    service.getMedicos = function (especialidades, callback) {
        if (!Array.isArray(especialidades))
            especialidades = [especialidades];

        $http.post($rootScope.apiDomain + '/info/getmedicos', especialidades).then(function (response) {
            callback(response.data);
        });
    }

    service.getAgendaPaciente = function(callback){
      $http.get($rootScope.apiDomain + '/paciente/agenda').then(function (response) {
          callback(response.data);
      });
    }

    return service;
}]);
