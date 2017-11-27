var app = angular.module('Consultorio', ['LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'ngAnimate', 'ui.router']);

// Configura as rotas do site
app.config(function ($stateProvider, $locationProvider /*, $urlRouteProvider */) {
    //$urlRouteProvider.otherwise('/');

    $stateProvider.state("home", {
        controller: "homeController",
        templateUrl: "/app/common/views/home.html",
        resolve: {
            access: ["access", function (access) { return access.isAnonymous(); }]
        }
    });

    $stateProvider.state("login", {
        controller: "loginController",
        templateUrl: "/app/common/views/login.html",
        resolve: {
            access: ["access", function (access) { return access.isAnonymous(); }]
        }
    });

    $stateProvider.state("cadastro", {
        controller: "cadastroController",
        templateUrl: "/app/common/views/cadastro.html",
        resolve: {
            access: ["access", function (access) { return access.isAnonymous(); }]
        }
    });

    $stateProvider.state("dashboard", {
        controller: "dashboardController",
        templateUrl: "/app/paciente/views/dashboard.html",
        resolve: {
            access: ["access", function (access) { return access.hasRole("paciente"); }]
        }
    });

    $stateProvider.state("conta", {
        controller: "contaController",
        templateUrl: "/app/paciente/views/conta.html",
        resolve: {
            access: ["access", function (access) { return access.hasRole("paciente"); }]
        }
    });

    $stateProvider.state("consultas", {
        controller: "consultasController",
        templateUrl: "/app/paciente/views/consultas.html",
        resolve: {
            access: ["access", function (access) { return access.hasRole("paciente"); }]
        }
    });

    $stateProvider.state("admin/cadastro", {
        controller: "cadastroMedicoController",
        templateUrl: "/app/admin/views/cadastroMedico.html",
        resolve: {
            access: ["access", function (access) { return access.hasRole("admin"); }]
        }
    });

    $locationProvider.html5Mode(true); //Remove '#' da URL.
}).run(['$rootScope', '$state', 'access', 'authService', function ($rootScope, $state, access, authService) {
    $rootScope.$on("$stateChangeError", function (event, toState, toParams, fromState, fromParams, error) {
        switch (error) {
            case access.UNAUTHORIZED:
                $state.go("cadastro");
                break;

            case access.FORBIDDEN:
                state.go("proibido");
                break;
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
