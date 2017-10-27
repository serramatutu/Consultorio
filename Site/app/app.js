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
});

// Configura os interceptadores
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);
