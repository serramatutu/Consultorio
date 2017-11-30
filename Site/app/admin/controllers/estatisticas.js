'use strict';
app.controller('consultaMensalPorMedicoController', ['estatisticaService', function (estatisticaService) {
    estatisticaService.getEstatisticaMedico().then(function(response) {
        this._estatistica = response.data;
    });

    this.labels = [];
    this.data = [];
    for (let i = 0; i < this._estatistica.length; i++) {
        this.labels.push(this._estatistica[i].medico.nome);
        this.data.push(this._estatistica[i].consultasNoMes);
    }
}]);