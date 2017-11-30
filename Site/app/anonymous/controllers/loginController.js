'use strict';
app.controller('loginController', ['$scope', '$state', 'authService', 'userState', function ($scope, $state, authService, userState) {
    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function () {
        // Quando logou, redireciona para a dashboard
        authService.login($scope.loginData).then(function (response) {
            userState.getState().then(function (auth) {
                if (auth.hasRole("paciente"))
                    $state.go("paciente.dashboard");
                else if (auth.hasRole("medico"))
                    $state.go("medico.dashboard");
                else if (auth.hasRole("admin"))
                    $state.go("admin.dashboard");
                else
                    $state.go("anonymous.cadastro");
            });
        },
        function (err) {
            $scope.message = err.data.error_description;
        });
    };
}]);