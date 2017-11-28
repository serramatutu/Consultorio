'use strict';
app.factory('utilitiesService', [function () {
    var serviceFactory = {};

    serviceFactory.addMinutes = function (date, minutes) {
        return new Date(date.getTime() + minutes * 60000);
    }

    return serviceFactory;
}]);