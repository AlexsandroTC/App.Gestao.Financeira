using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Gestao.Financeira.Domain;
using App.Gestao.Financeira.ViewModel.Extract;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Gestao.Financeira.View.Extract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditExtractView : ContentPage
    {
        public EditExtractView(Transacao trasacao)
        {
            BindingContext = new EditExtractViewModel(trasacao);
            InitializeComponent();
        }
    }
}