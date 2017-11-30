'use strict';

app.controller('consultaMensalPorMedicoController', ['$element', 'estatisticaService', function ($element, estatisticaService) {
    estatisticaService.getEstatisticaMedico().then(function(response) {
        var estatistica = response.data;
        var data = {
            labels: null,
            datasets: [
                {
                    label: "Consultas no mês",
                    data: []
                }
            ]
        };

        var labels = [];
        var chartData = [];
        for (let i = 0; i < estatistica.length; i++) {
            labels.push(estatistica[i].medico.nome);
            chartData.push(estatistica[i].consultasNoMes);
        }

        data.labels = labels;
        data.datasets[0].data = chartData;

        var chart = new Chart(
            $element.find('#chart')[0].getContext('2d'),
            {
                type: 'bar',
                data: data,
                options: {
                    cutoutPercentage: 50,
                    animation: {
                        animateScale: true,
                        animateRotate: false
                    },
                    title: {
                        display: true,
                        text: 'Consultas mensais por médico'
                    }
                }
            }
        );
    });
}]);