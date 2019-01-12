using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSetUpDTL : ContentPage
    {
        public string Courriel;
        public TradoÉchange echange;
        public PageSetUpDTL(TradoÉchange tradoÉchange, string courriel)
        {
            InitializeComponent();
        }
    }
}