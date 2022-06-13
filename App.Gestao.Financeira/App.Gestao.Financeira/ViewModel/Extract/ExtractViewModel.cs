using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Gestao.Financeira.Configuration;
using App.Gestao.Financeira.Domain;

namespace App.Gestao.Financeira.ViewModel.Extract
{
    public class ExtractViewModel : BaseViewModel
    {
        private static Database database;
        public ObservableCollection<Transacao> TransacaoList { get; private set; } = new ObservableCollection<Transacao>();
        
        public ExtractViewModel()
        {
            var path= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "finance.db3");
            database = new Database(path);
            
            InitialLoadTransacaoAsync();
        }
        
        private async Task InitialLoadTransacaoAsync()
        {
            var transacoes = await database.GetTrascaoAsync();

            var list = transacoes.OrderBy(c => c.DataLancamento).ToList();
            TransacaoList.Clear();
            transacoes.ForEach(c =>
            {
                TransacaoList.Add(c);
            });
        }
    }
}