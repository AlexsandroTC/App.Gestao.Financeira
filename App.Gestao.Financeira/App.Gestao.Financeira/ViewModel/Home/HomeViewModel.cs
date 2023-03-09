using App.Gestao.Financeira.Configuration;
using App.Gestao.Financeira.Domain;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using App.Gestao.Financeira.Enum;

namespace App.Gestao.Financeira.ViewModel.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private static Database database;

        public Command AdicionarRendimentoCommand { get; }
        public Command AdicionarDespesaCommand { get; }
        public Command RefreshLancamentosCommand { get; }

        #region
        public ObservableCollection<Transacao> TransacaoList { get; private set; } = new ObservableCollection<Transacao>();

        private decimal totalEntrada;
        public decimal TotalEntrada
        {
            get => totalEntrada;
            set => SetProperty(ref totalEntrada, value);
        }
        
        private decimal totalSaida;
        public decimal TotalSaida
        {
            get => totalSaida;
            set => SetProperty(ref totalSaida, value);
        }
        
        private decimal valorSaldo;
        public decimal ValorSaldo
        {
            get => valorSaldo;
            set => SetProperty(ref valorSaldo, value);
        }
        
        private decimal? valor;
        public decimal? Valor
        {
            get => valor;
            set => SetProperty(ref valor, value);
        }
        
        private string descricao;
        public string Descricao
        {
            get => descricao;
            set => SetProperty(ref descricao, value);
        }
        #endregion

        public HomeViewModel()
        {
            var path= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "finance.db3");
            database = new Database(path);
            
            InitialLoadTransacaoAsync();

            AdicionarDespesaCommand = new Command(() => AdicionarDespesaAsync());
            AdicionarRendimentoCommand = new Command(() => AdicionarRendimentoAsync());
            RefreshLancamentosCommand = new Command(() => RefreshLancamentosAsync());
        }

        
        private async Task AdicionarRendimentoAsync()
        {
            if (valor != null && !string.IsNullOrWhiteSpace(descricao))
            {
                var trancasao = new Transacao();
                trancasao.Descricao = descricao;
                trancasao.Valor = (decimal)valor;
                trancasao.Tipo = TipoTransacao.Entrada;

                var id = await database.SaveTrancaosaoAsync(trancasao);

                await Application.Current.MainPage.DisplayToastAsync("Entrada registrada com sucesso.");

                if (valor > 0)
                {
                    TotalEntrada += (decimal)valor;
                }

                ValorSaldo = totalEntrada - totalSaida;
                Valor = null;
                Descricao = null;

                await LoadTransacaoAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Faltou alguns dados", "Faltou informar algum dados para realizar um lançamento.", "Ok");
            }
        }

        private async Task AdicionarDespesaAsync()
        {
           
            if (valor != null && !string.IsNullOrWhiteSpace(descricao))
            {
                var trancasao = new Transacao();
                trancasao.Descricao = descricao;
                trancasao.Valor = (decimal)valor;
                trancasao.Tipo = TipoTransacao.Saida;

                var id = await database.SaveTrancaosaoAsync(trancasao);

                TotalSaida += (decimal)valor;

                ValorSaldo = totalEntrada - totalSaida;
                Valor = null;
                Descricao = null;

                await LoadTransacaoAsync(); 
                await Application.Current.MainPage.DisplayToastAsync("Despesa registrada com sucesso."); ;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Faltou alguns dados", "Faltou informar algum dados para realizar um lançamento.", "Ok");
            }
        }

        private async Task LoadTransacaoAsync()
        {
            var transacoes = await database.GetTrascaoAsync();

            TransacaoList.Clear();
            transacoes.ForEach(c =>
            {
                TransacaoList.Add(c);
            });
        }

        private async Task InitialLoadTransacaoAsync()
        {
            var transacoes = await database.GetTrascaoAsync();

            var list = transacoes.OrderByDescending(c => c.DataLancamento).Where(x => x.Estornado == false).ToList();
            
            TotalEntrada = list.Where(c => c.Tipo == TipoTransacao.Entrada).Sum(x => x.Valor);
            TotalSaida = list.Where(c => c.Tipo == TipoTransacao.Saida).Sum(x => x.Valor);
            ValorSaldo = totalEntrada - totalSaida;

            TransacaoList.Clear();
            list.ForEach(c =>
            {
                TransacaoList.Add(c);
            });

        }

        private async Task RefreshLancamentosAsync() 
        {
            await InitialLoadTransacaoAsync();
        }
    }
}