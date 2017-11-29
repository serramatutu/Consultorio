'use strict';
app.factory('adminService', ['$rootScope', '$http', function ($rootScope, $http) {
    var serviceFactory = {};

    serviceFactory.cadastrarMedico = function (loginData, userData) {
        var data = {
            userModel: loginData,
            medico: userData
        };

        return $http.post($rootScope.apiDomain + '/admin/cadastrarmedico', data);
    }

    return serviceFactory;
}]);
