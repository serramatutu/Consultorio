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
                    scales: {
                        yAxes: [{
                            display: true,
                            ticks: {
                                beginAtZero: true,
                                stepSize: 1
                            }
                        }]
                    },
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

app.controller('consultaMensalPorPacienteController', ['$element', 'estatisticaService', function ($element, estatisticaService) {
    estatisticaService.getEstatisticaPaciente().then(function (response) {
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
            labels.push(estatistica[i].paciente.nome);
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
                    scales: {
                        yAxes: [{
                            display: true,
                            ticks: {
                                beginAtZero: true,
                                stepSize: 1
                            }
                        }]
                    },
                    cutoutPercentage: 50,
                    animation: {
                        animateScale: true,
                        animateRotate: false
                    },
                    title: {
                        display: true,
                        text: 'Consultas mensais por paciente'
                    }
                }
            }
        );
    });
}]);

app.controller('consultasPorEspecialidadeController', ['$element', 'estatisticaService', function ($element, estatisticaService) {
    estatisticaService.getEstatisticaEspecialidade().then(function (response) {
        var estatistica = response.data;
        var data = {
            labels: null,
            datasets: [
                {
                    label: "Consultas hoje",
                    data: []
                }
            ]
        };

        var labels = [];
        var chartData = [];
        for (let i = 0; i < estatistica.length; i++) {
            labels.push(estatistica[i].especialidade.nome);
            chartData.push(estatistica[i].consultasNoMes);
        }

        data.labels = labels;
        data.datasets[0].data = chartData;

        var chart = new Chart(
            $element.find('#chart')[0].getContext('2d'),
            {
                type: 'pie',
                data: data,
                options: {
                    cutoutPercentage: 50,
                    animation: {
                        animateScale: true,
                        animateRotate: false
                    },
                    title: {
                        display: true,
                        text: 'Consultas por especialidade hoje'
                    }
                }
            }
        );
    });
}]);

app.controller('consultaCanceladaMensalmenteController', ['$element', 'estatisticaService', function ($element, estatisticaService) {
    estatisticaService.getEstatisticaMesConsulta().then(function (response) {
        var estatistica = response.data;

        var labels = [];
        var canceladas = [];
        var realizadas = [];
        for (let i = 0; i < estatistica.length; i++) {
            labels.push(estatistica[i].mes);
            canceladas.push(estatistica[i].consultasCanceladasNoMes);
            realizadas.push(estatistica[i].consultasNoMes);
        }

        var data = {
            labels: labels,
            datasets: [
                {
                    label: "Consultas canceladas",
                    data: canceladas
                },
                {
                    label: "Consultas realizadas",
                    data: realizadas
                }
            ]
        };

        var chart = new Chart(
            $element.find('#chart')[0].getContext('2d'),
            {
                type: 'bar',
                data: data,
                options: {
                    scales: {
                        yAxes: [{
                            display: true,
                            ticks: {
                                beginAtZero: true,
                                stepSize: 1
                            }
                        }]
                    },
                    cutoutPercentage: 50,
                    animation: {
                        animateScale: true,
                        animateRotate: false
                    },
                    title: {
                        display: true,
                        text: 'Consultas canceladas no mês'
                    }
                }
            }
        );
    });
}]);