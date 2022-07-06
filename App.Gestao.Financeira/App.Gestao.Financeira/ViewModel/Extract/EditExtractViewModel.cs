using System;
using System.Threading.Tasks;
using App.Gestao.Financeira.Configuration;
using App.Gestao.Financeira.Domain;
using Xamarin.Forms;
using System.IO;
using Xamarin.CommunityToolkit.Extensions;

namespace App.Gestao.Financeira.ViewModel.Extract
{
    public class EditExtractViewModel : BaseViewModel
    {
        private readonly Transacao _transacao;
        private static Database _database;
        private INavigation _navigation => Application.Current.MainPage.Navigation;
        
        private string descricao;
        public string Descricao
        {
            get => descricao;
            set => SetProperty(ref descricao, value);
        }
        
        private decimal? valor;
        public decimal? Valor
        {
            get => valor;
            set => SetProperty(ref valor, value);
        }
        
        private DateTime dataEdicao;
        public DateTime DataEdicao
        {
            get => dataEdicao;
            set => SetProperty(ref dataEdicao, value);
        }


        private string categoria;
        public string Categoria
        {
            get => categoria;
            set => SetProperty(ref categoria, value);
        }
        
        private bool estornar;
        public bool Estornar
        {
            get => estornar;
            set => SetProperty(ref estornar, value);
        }
        
        public Command EditarLancamentoCommand { get; }
        
        public EditExtractViewModel(Transacao transacao)
        {
            _transacao = transacao;

            ShowInfomation();
         
            var path= Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "finance.db3");
            _database = new Database(path);
            
            EditarLancamentoCommand = new Command(() => EditarLancamentoAsync()); 
        }

        private async Task EditarLancamentoAsync()
        {
            var trasacao = _transacao;
            trasacao.Categoria = Categoria;
            trasacao.Estornado = Estornar;

            var confirm = await Application.Current.MainPage.DisplayAlert("Confirmar alteração?","Você confirma a ataulização as informações do lançamento.","Sim","Não");
            if (confirm)
            {
                await _database.UpdateTransacaoAsync(trasacao);
                _navigation.PopAsync();
                
                await Application.Current.MainPage.DisplayToastAsync("Lançamento atualizado com sucesso.");
            }
        }

        private void ShowInfomation()
        {
            Descricao = _transacao.Descricao;
            Valor = _transacao.Valor;
            Estornar = _transacao.Estornado;
            DataEdicao = _transacao.DataUltimaEdicao;
            Categoria = _transacao.Categoria;
        }
    }
}