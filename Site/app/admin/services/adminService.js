'use strict';
app.factory('adminService', ['$rootScope', '$http', function ($rootScope, $http) {
    var serviceFactory = {};

    serviceFactory.cadastrarMedico = function (registrationData) {
        return $http.post($rootScope.apiDomain + '/admin/cadastrarmedico', registrationData);
    }

    return serviceFactory;
}]);
