var app = angular.module('Consultorio', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'ngAnimate']);

// Configura as rotas do site
app.config(function ($routeProvider, $locationProvider) {
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
        templateUrl: "/app/views/dashboard.html",
        resolve: {
            loggedIn: permitirLogado
        }
    });

    $routeProvider.when("/agendar", {
        controller: "agendamentoController",
        templateUrl: "/app/views/agendar.html",
        resolve: {
            loggedIn: permitirLogado
        }
    });

    // Caso url não seja válida, redireciona para o devido lugar
    $routeProvider.otherwise('/home');

    // Habilitar isso depois
    $locationProvider.html5Mode(true); //Remove '#' da URL.
});

// Configura os interceptadores
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);

app.run(['$rootScope', '$location', function ($rootScope, $location) {
    $rootScope.$on('$locationChangeStart', function () {
        $rootScope.path = $location.path();
    });
}]);

// Não permite endereços não autorizados
//app.run(['$rootScope', '$location', 'authService', function ($rootScope, $location, authService) {
//    $rootScope.$on('$routeChangeStart', function (event) {
//        if (!authService.isAuthenticated) { // Se não tiver logado
//            console.log('ROTA NÃO PERMITIDA PARA USUÁRIO ANÔNIMO');
//            event.preventDefault();
//            $location.path('/login');
//        }
//    });
//}]);