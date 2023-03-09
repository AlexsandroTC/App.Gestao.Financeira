using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Gestao.Financeira.Configuration;
using App.Gestao.Financeira.Domain;
using Microcharts;
using SkiaSharp;

namespace App.Gestao.Financeira.ViewModel.Planning
{
    public class PlanningViewModel : BaseViewModel
    {
        private static Database database;
        private List<Transacao> _transacaoList { get; set; } = new List<Transacao>();

        Chart _chart;
        public Chart Chart
        {
            get => _chart;
            private set => SetProperty(ref _chart, value);
        }

        Chart _linearChart;
        public Chart LinearChart
        {
            get => _linearChart;
            private set => SetProperty(ref _linearChart, value);
        }

        public PlanningViewModel()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "finance.db3");
            database = new Database(path);

            Task task = InitialLoadTransacaoAsync();
            Chart = GetPizzaChart();
            LinearChart = GetLineChart();
        }

        private Chart GetPizzaChart()
        {
            var entries = new List<ChartEntry>
            {
                new ChartEntry(1135)
                {
                    ValueLabel = "R$ 1135,00",
                    Label = "Moradia",
                    Color = SKColor.Parse("#f2be44"),
                    TextColor = SKColor.Parse("#f2be44")
                },
                new ChartEntry(200)
                {
                    ValueLabel = "R$ 200,00",
                    Label = "Combustível",
                    Color = SKColor.Parse("#298a0b"),
                    TextColor = SKColor.Parse("#298a0b")
                },
                new ChartEntry(100)
                {
                    ValueLabel = "R$ 100,00",
                    Label = "Academia",
                    Color = SKColor.Parse("#8c2f07"),
                    TextColor = SKColor.Parse("#8c2f07")
                },
                new ChartEntry(500)
                {
                    ValueLabel = "R$ 500,00",
                    Label = "Carro",
                    Color = SKColor.Parse("#51524e"),
                    TextColor = SKColor.Parse("#51524e")
                },
                new ChartEntry(400)
                {
                    ValueLabel = "R$ 400,00",
                    Label = "Outros",
                    Color = SKColor.Parse("#17736e"),
                    TextColor = SKColor.Parse("#17736e")
                }
            };

            return new DonutChart { Entries = entries, LabelTextSize = 38 };
        }

        public Chart GetLineChart()
        {
            var chart = new BarChart()
            {
                Entries = new[]
                {
                    new ChartEntry(1300)
                    {
                        Label = "Entradas",
                        ValueLabel = "4179",
                        Color = SKColor.Parse("#2c3e50")
                    },
                    new ChartEntry(500)
                    {
                        Label = "Saidas",
                        ValueLabel = "1793",
                        Color = SKColor.Parse("#77d065")
                    }
                }
            };

            chart.LabelTextSize = 38;
            chart.LabelOrientation = Orientation.Horizontal;
            chart.ValueLabelOrientation = Orientation.Horizontal;
            chart.IsAnimated = true;

            return chart;
        }

        public async Task InitialLoadTransacaoAsync()
        {
            var lsit = await database.GetTrascaoAsync();
            _transacaoList = lsit.Where(c => c.Estornado == false).ToList();
        }
    }
}
