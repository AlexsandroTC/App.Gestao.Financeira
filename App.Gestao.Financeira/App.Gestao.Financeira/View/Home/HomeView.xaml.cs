using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Gestao.Financeira.View.Home
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentView
    {
        public HomeView()
        {
            BindingContext = new ViewModel.Home.HomeViewModel();
            InitializeComponent();
        }
    }
}