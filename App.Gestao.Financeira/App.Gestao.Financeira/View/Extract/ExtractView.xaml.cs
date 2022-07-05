using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Gestao.Financeira.View.Extract
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtractView : ContentView
    {
        public ExtractView()
        {
            BindingContext = new ViewModel.Extract.ExtractViewModel();
            InitializeComponent();
        }
    }
}