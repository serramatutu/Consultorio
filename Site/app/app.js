var app = angular.module('Consultorio', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap']);

// Configura as rotas do site
app.config(function ($routeProvider, $locationProvider) {

    $locationProvider.hashPrefix(''); // Tira o bug do #/ virar %2F

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/cadastro", {
        controller: "cadastroController",
        templateUrl: "/app/views/cadastro.html"
    });

    $routeProvider.when("/dashboard", {
        controller: "dashboardController",
        templateUrl: "/app/views/dashboard.html"
    });

    // Caso url não seja válida, redireciona para home
    $routeProvider.otherwise({ redirectTo: "/home" });

    $locationProvider.html5Mode(true); //Remove '#' da URL.
});

// Configura os interceptadores
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

// Não permite endereços não autorizados
app.run(['$rootScope', '$location', 'authService', function ($rootScope, $location, authService) {
    $rootScope.$on('$routeChangeStart', function (event) {
        if (!authService.isAuthenticated) { // Se não tiver logado
            console.log('ROTA NÃO PERMITIDA PARA USUÁRIO ANÔNIMO');
            event.preventDefault();
            $location.path('/login');
        }
    });
}]);