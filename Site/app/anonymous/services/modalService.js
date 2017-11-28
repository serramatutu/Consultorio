app.factory('modalService', ['$uibModal', function ($modal) {
    var factory = {};

    factory.showModal = function (customModalDefaults) {
        if (!customModalDefaults)
            customModalDefaults = {};
        customModalDefaults.backdrop = 'static';
        return factory.show(customModalDefaults);
    };

    factory.show = function (customModalDefaults) {
        //Create temp objects to work with since we're in a singleton service
        var tempModalDefaults = Object.assign(customModalDefaults);

        if (!tempModalDefaults.controller) {
            tempModalDefaults.controller = function ($scope, $modalInstance) {
                $scope.modalOptions = tempModalOptions;
                $scope.modalOptions.ok = function (result) {
                    $modalInstance.close(result);
                };
                $scope.modalOptions.close = function (result) {
                    $modalInstance.dismiss('cancel');
                };
            }
        }

        return $modal.open(tempModalDefaults).result;
    };

    return factory
}]);
