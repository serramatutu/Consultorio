'use strict';

// Determina um interceptador de requisições que adiciona o bearer token ao header caso esteja logado
app.factory('authInterceptorService', ['$q', '$location', 'localStorageService', function ($q, $location, localStorageService) {
    var authInterceptorServiceFactory = {};

    var _request = function (config) {
        config.headers = config.headers || {};

        // Pega as informações de autorização ao server
        var authData = localStorageService.get('authorizationData');
        if (authData)
            config.headers.Authorization = 'Bearer ' + authData.token; // Adiciona o token ao header

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) // Login não autorizado
            $location.path('/login');
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);