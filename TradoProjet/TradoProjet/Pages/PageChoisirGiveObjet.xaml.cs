using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradoProjet.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageChoisirGiveObjet : ContentPage
	{
	    public TradoObjet[] tradoObjetGet;
	    public TradoObjet[] tradoObjetGive;
	    public string Courriel;
		public PageChoisirGiveObjet (TradoObjet[] tradoObjetGets, TradoObjet[] tradoObjetGives, string courriel)
		{
			InitializeComponent ();
		    Courriel = courriel;
		    tradoObjetGet = tradoObjetGets;
		    tradoObjetGive = tradoObjetGives;
		}

	    private static List<TradoObjet> liste = new List<TradoObjet>();
        protected override async void OnAppearing()
	    {
            base.OnAppearing();
	        liste = await Trado.serviceMobile.GetTable<TradoObjet>().ToListAsync();
	        ListView.ItemsSource = liste;
	    }

        TradoObjet objetSelectionne = new TradoObjet();
	    private void Select_OnClicked(object sender, SelectedItemChangedEventArgs e)
	    {
	        objetSelectionne = (TradoObjet)e.SelectedItem;
	        tradoObjetGive[0] = objetSelectionne;
            Navigation.PushAsync(new PageAjouterOffre(tradoObjetGet, tradoObjetGive, Courriel));
	    }

	    private void Annule_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new PageAjouterOffre(tradoObjetGet, tradoObjetGive, Courriel));
	    }
	}
}