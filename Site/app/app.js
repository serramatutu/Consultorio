var app = angular.module('Consultorio', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'ngAnimate']);

// Configura as rotas do site
app.config(function ($provide, $routeProvider, $locationProvider) {
    // Permite que se use o routeProvider em app.run
    $provide.factory('$routeProvider', function () {
        return $routeProvider;
    });

    // Permite apenas entrada de usuário logado
    var permitirLogado = function ($location, $q, authService) {
        var d = $q.defer();
        if (authService.auth.isAuthenticated) {
            d.resolve(); // Caso esteja logado
        } else {
            d.reject(); // Caso não esteja logado
            $location.url('/login');
        }
        return d.promise;
    };

    var naoPermitirLogado = function ($location, $q, authService) {
        var d = $q.defer();
        if (!authService.auth.isAuthenticated) {
            d.resolve(); // Caso não esteja logado
        } else {
            d.reject(); // Caso esteja logado
            $location.url('/dashboard');
        }
        return d.promise;
    };

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/common/views/home.html",
        resolve: {
            loggedIn: naoPermitirLogado
        }
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/common/views/login.html"
    });

    $routeProvider.when("/cadastro", {
        controller: "cadastroController",
        templateUrl: "/app/common/views/cadastro.html",
        resolve: {
            loggedIn: naoPermitirLogado
        }
    });

    $routeProvider.when("/dashboard", {
        controller: "dashboardController",
        templateUrl: "/app/paciente/views/dashboard.html",
        resolve: {
            loggedIn: permitirLogado
        }
    });

    $routeProvider.when("/conta", {
        controller: "contaController",
        templateUrl: "/app/paciente/views/conta.html",
        resolve: {
            loggedIn: permitirLogado
        }
    });

    $routeProvider.when("/consultas", {
        controller: "consultasController",
        templateUrl: "/app/paciente/views/consultas.html",
        resolve: {
            loggedIn: permitirLogado
        }
    });

    // Habilitar isso depois
    $locationProvider.html5Mode(true); //Remove '#' da URL.
}).run(['$routeProvider', 'authService', function ($routeProvider, authService) {
    authService.fillAuthData();

    // Caso url não seja válida, redireciona para o devido lugar
    $routeProvider.otherwise({ // TODO: Diferente para admin, paciente e medico
        redirectTo: function () {
            if (authService.auth.isAuthenticated)
                return '/dashboard';

            return '/home';
        }
    });
}]);

// Configura os interceptadores
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['$rootScope', '$location', function ($rootScope, $location) {
    $rootScope.$on('$locationChangeStart', function () {
        $rootScope.path = $location.path();
    });
}]);

// Define as constantes do aplicativo
app.run(['$rootScope', function ($rootScope) {
    $rootScope.apiDomain = 'http://localhost:58949';
}])
