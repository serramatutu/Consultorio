app.controller('adminDashboardController', ['$scope', 'modalService',
    function ($scope, modalService) {
    $scope.cadastrarMedico = function (consulta) {
        var modalDefaults = {
            templateUrl: "/app/admin/modals/cadastroMedicoModal.html",
            controller: "cadastroMedicoController",
            size: 'lg'
        };

        modalService.showModal(modalDefaults);
    }

    $scope.cadastrarEspecialidade = function (consulta) {
        var modalDefaults = {
            templateUrl: "/app/admin/modals/cadastroEspecialidadeModal.html",
            controller: "cadastroEspecialidadeController",
            size: 'lg'
        };

        modalService.showModal(modalDefaults);
    }
}]);