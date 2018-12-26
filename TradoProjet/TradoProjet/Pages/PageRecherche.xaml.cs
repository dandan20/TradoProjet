using System;
using System.Collections.Generic;
using System.Linq;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradoProjet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageRecherche : ContentPage
	{
	    public string Courriel;
		public PageRecherche (string courriel)
		{
			InitializeComponent ();
		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var liste = await Trado.serviceMobile.GetTable<TradoObjet>().ToListAsync();
            ObjetsListView.ItemsSource = liste;
        }

        //Fonction activé quand la recherche est faite.
        private void RechercheSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
        }

        TradoObjet objetSelectionne = new TradoObjet();
        private void ObjetsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            objetSelectionne = (TradoObjet)e.SelectedItem;
            Navigation.PushAsync(new PageObjet(objetSelectionne, Courriel));
        }

        private async void RechercheSearchBar_TextChangedAsync(object sender, TextChangedEventArgs e)
        {
            //Rechercher
            var liste = await Trado.serviceMobile.GetTable<TradoObjet>().ToListAsync();
            var resultat = liste.Where(x => x.Nom.ToUpper().Contains(RechercheSearchBar.Text.ToUpper())).ToList();
            if (resultat == null)
            {
                ObjetsListView.ItemsSource = liste;
            }
            else
            {
                ObjetsListView.ItemsSource = resultat;
            }
        }
    }
}