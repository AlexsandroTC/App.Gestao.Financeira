using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.Gestao.Financeira.View.Component
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransacaoExtrato : Grid
    {
        public TransacaoExtrato()
        {
            InitializeComponent();
        }
    }
}