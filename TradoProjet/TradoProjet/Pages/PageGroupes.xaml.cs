using Model.Entities;
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
	public partial class PageGroupes : ContentPage
	{
		public PageGroupes ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var liste = await Trado.serviceMobile.GetTable<TradoGroup>().ToListAsync();
            GroupesListe.ItemsSource = liste;
        }
    }
}