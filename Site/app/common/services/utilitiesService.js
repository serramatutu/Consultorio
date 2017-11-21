'use strict';
app.factory('utilitiesService', [function () {
    var service = {};

    service.formatarData = function (data) {
        var dia = data.getDate();
        var mes = data.getMonth();
        var ano = data.getFullYear();

        return dia + '/' + mes + '/' + ano;
    }

    service.formatarHora = function (data) {
        var hora = data.getHours();
        var min = data.getMinutes();

        return ("0" + hora).slice(-2) + 'h' + ("0" + min).slice(-2) + 'min';
    }

    service.addMinutes = function (date, minutes) {
        return new Date(date.getTime() + minutes * 60000);
    }

    return service;
}]);