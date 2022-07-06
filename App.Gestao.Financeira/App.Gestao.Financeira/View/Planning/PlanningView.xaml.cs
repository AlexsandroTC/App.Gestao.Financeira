using App.Gestao.Financeira.ViewModel.Planning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Gestao.Financeira.View.Planning
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanningView : ContentView
    {
        private PlanningViewModel _viewModel;

        public PlanningView()
        {
            BindingContext = new ViewModel.Planning.PlanningViewModel(); ;
            InitializeComponent();
        }
    }
}