﻿var app = angular.module('Consultorio',
    ['LocalStorageModule',
     'angular-loading-bar',
     'ui.bootstrap',
     'ngAnimate',
     'ui.router']);


// Configura as rotas do site
app.config(['$stateProvider', '$locationProvider', '$urlRouterProvider',
    function ($stateProvider, $locationProvider, $urlRouterProvider) {

    // TODO: Chart options

    $urlRouterProvider.when("/", ['$state', 'access', function ($state, access) {
        access.getStatic().then(function (userData) {
            if (userData.hasRole("paciente"))
                $state.go("paciente.dashboard");
            else if (userData.hasRole("medico"))
                $state.go("medico.dashboard");
            else if (userData.hasRole("admin")){
                console.log('batata');
                $state.go("admin.dashboard");
            }
            else
                $state.go("anonymous.home");
        });
    }]);
    $urlRouterProvider.otherwise("/notfound");

    var logoutController = ['$scope', 'authService', function ($scope, authService) {
        $scope.logOut = authService.logOut;
    }];

    $stateProvider.state("unauthorized", { // 401
        url: '/unauthorized',
        templateUrl: 'app/error.html',
        controller: function ($scope) {
            $scope.error = {
                code: '401',
                name: 'Não autorizado',
                message: 'Faça login para acessar este recurso.'
            }
        }
    })
    .state("forbidden", { // 403
        url: '/forbidden',
        templateUrl: 'app/error.html',
        controller: function ($scope) {
            $scope.error = {
                code: '403',
                name: 'Proibido',
                message: 'Seu tipo de login não permite acesso a este recurso.'
            }
        }
    })
    .state("notfound", { // 404
        url: '/notfound',
        templateUrl: 'app/error.html',
        controller: function ($scope) {
            $scope.error = {
                code: '404',
                name: 'Não encontrado',
                message: 'Tem certeza de que digitou tudo certo?'
            }
        }
    })


    $stateProvider.state("anonymous", {
        abstract: true,
        templateUrl: "app/anonymous/anonymous.html",
        resolve: {
            access: ["access", function (access) { return access.isAnonymous(); }]
        }
    })
    .state("anonymous.home", {
        url: '/home',
        controller: "homeController",
        templateUrl: "/app/anonymous/views/home.html"
    })
    .state("anonymous.login", {
        url: '/login',
        controller: "loginController",
        templateUrl: "/app/anonymous/views/login.html",
    })
    .state("anonymous.cadastro", {
        url: '/cadastro',
        controller: "cadastroController",
        templateUrl: "/app/anonymous/views/cadastro.html",
    });

    $stateProvider.state("paciente", {
        abstract: true,
        templateUrl: "app/paciente/paciente.html",
        controller: logoutController,
        resolve: {
            access: ["access", function (access) { return access.hasRole("paciente"); }]
        }
    })
    .state("paciente.dashboard", {
        url: '/dashboard',
        controller: "pacienteDashboardController",
        templateUrl: "/app/paciente/views/dashboard.html",
    })
    .state("paciente.conta", {
        url: '/conta',
        controller: "contaController",
        templateUrl: "/app/paciente/views/conta.html",
    })
    .state("paciente.consultas", {
        url: '/consultas',
        controller: "consultasController",
        templateUrl: "/app/paciente/views/consultas.html",
    });

    $stateProvider.state("admin", {
        abstract: true,
        url: '/admin',
        controller: logoutController,
        templateUrl: '/app/admin/admin.html',
        resolve: {
            access: ["access", function (access) { return access.hasRole("admin"); }]
        }
    })
    .state("admin.dashboard", {
        url: '/dashboard',
        controller: 'adminDashboardController',
        templateUrl: "/app/admin/views/dashboard.html"
    })
    .state("admin.consultas", {
        url: '/consultas',
        controller: 'adminConsultasController',
        templateUrl: "/app/admin/views/consultas.html"
    });

    $stateProvider.state("medico", {
        abstract: true,
        url: '/medico',
        controller: 'medicoController',
        templateUrl: '/app/medico/medico.html',
        resolve: {
            access: ["access", function (access) { return access.hasRole("medico"); }]
        }
    })
    .state("medico.dashboard", {
        url: '/dashboard',
        controller: 'medicoDashboardController',
        templateUrl: "/app/medico/views/dashboard.html"
    })
    .state("medico.consultas", {
        url: '/consultas',
        controller: 'medicoConsultasController',
        templateUrl: "/app/medico/views/consultas.html"
    })

    $locationProvider.html5Mode(true); //Remove '#' da URL.
    
}]).run(['$rootScope', '$state', 'access', 'authService', function ($rootScope, $state, access, authService) {
    $rootScope.$on("$stateChangeError", function (event, toState, toParams, fromState, fromParams, error) {
        switch (error) {
            case access.UNAUTHORIZED:
                $state.go("unauthorized");
                break;

            case access.FORBIDDEN:
                state.go("forbidden");
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
