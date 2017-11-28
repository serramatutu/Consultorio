'use strict';
app.factory('authService', ['$rootScope', '$http', '$q', 'localStorageService',
    function ($rootScope, $http, $q, localStorageService) {
    var authServiceFactory = {};

    var _cadastrar = function (registration) {
        _logOut();

        return $http.post($rootScope.apiDomain + '/conta/cadastro', registration).then(function success(response) {
            return response;
        });
    };

    var _alterar = function (userData) {
        return $http.post($rootScope.apiDomain + '/conta/alterar', userData).then(function success(response) {
            return response;
        });
    }

    var _login = function (loginData) {
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post($rootScope.apiDomain + '/token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then(
            function success(response) {
            localStorageService.set('authorizationData', { token: response.data.access_token });

            deferred.resolve(response);
        }, function error(err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _logOut = function () {
        localStorageService.remove('authorizationData');
    };

    var _getProfile = function () {
        return $http.get('http://localhost:58949/conta/getuserdata');
    }

    authServiceFactory.cadastrar = _cadastrar;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.getProfile = _getProfile;

    return authServiceFactory;
}]);