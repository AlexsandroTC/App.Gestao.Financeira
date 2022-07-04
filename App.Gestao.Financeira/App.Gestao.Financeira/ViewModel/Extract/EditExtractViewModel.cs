using App.Gestao.Financeira.Domain;

namespace App.Gestao.Financeira.ViewModel.Extract
{
    public class EditExtractViewModel : BaseViewModel
    {
        private readonly Transacao _transacao;
        
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

        
        public EditExtractViewModel(Transacao transacao)
        {
            _transacao = transacao;
            ShowInfomation();
        }

        private void ShowInfomation()
        {
            Descricao = _transacao.Descricao;
            Valor = _transacao.Valor;
        }
    }
}