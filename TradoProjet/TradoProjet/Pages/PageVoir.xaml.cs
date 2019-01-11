using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageVoir : ContentPage
    {
        public string Courriel;
        public PageVoir(string courriel)
        {
            InitializeComponent();
            Courriel = courriel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var liste = await Trado.serviceMobile.GetTable<TradoÉchange>().ToListAsync();
            var tradeGroups = from echange in liste
                              group echange by echange.createdAt;
            TradeList.ItemsSource = tradeGroups;
        }
    }
}