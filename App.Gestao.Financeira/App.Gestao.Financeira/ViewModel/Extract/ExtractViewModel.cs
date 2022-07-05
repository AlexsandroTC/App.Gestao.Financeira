using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Gestao.Financeira.Configuration;
using App.Gestao.Financeira.Domain;
using App.Gestao.Financeira.View.Extract;
using Xamarin.Forms;

namespace App.Gestao.Financeira.ViewModel.Extract
{
    public class ExtractViewModel : BaseViewModel
    {
        private INavigation Navigation => Application.Current.MainPage.Navigation;
        private static Database database;
        public Command RefreshLancamentosCommand { get; }
        public Command SelectItemCommand { get; }
        public ObservableCollection<Transacao> TransacaoList { get; private set; } = new ObservableCollection<Transacao>();
        public Transacao TrasactionSelectItem { get; set; }
        
        private decimal valorSaldo;
        public decimal ValorSaldo
        {
            get => valorSaldo;
            set => SetProperty(ref valorSaldo, value);
        }


        public ExtractViewModel()
        {
            var path= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "finance.db3");
            database = new Database(path);
            
            InitialLoadTransacaoAsync();
            RefreshLancamentosCommand = new Command(() => RefreshLancamentosAsync());
            SelectItemCommand = new Command(SelectItem);
        }

        private void SelectItem(object obj)
        {
            var transacao = obj as Transacao;
            Navigation.PushAsync(new EditExtractView(transacao));
        }

        private async Task InitialLoadTransacaoAsync()
        {
            var transacoes = await database.GetTrascaoAsync();

            var list = transacoes.OrderByDescending(c => c.DataLancamento).ToList();
            var totalEntrada = transacoes.Where(c => c.Tipo == 1).Sum(x => x.Valor);
            var totalSaida = transacoes.Where(c => c.Tipo == 2).Sum(x => x.Valor);
            ValorSaldo = totalEntrada - totalSaida;

            TransacaoList.Clear();
            list.ForEach(c =>
            {
                TransacaoList.Add(c);
            });
            TransacaoList.OrderByDescending(c => c.DataLancamento);
        }

        private async Task RefreshLancamentosAsync()
        {
            await InitialLoadTransacaoAsync();
        }
    }
}