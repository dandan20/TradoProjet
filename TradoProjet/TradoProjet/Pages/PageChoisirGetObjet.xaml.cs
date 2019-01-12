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
	public partial class PageChoisirGetObjet : ContentPage
	{
	    public TradoObjet[] tradoObjetGet;
	    public TradoObjet[] tradoObjetGive;
	    public string MyCourriel;
        public string HisCourriel;
		public PageChoisirGetObjet (TradoObjet[] tradoObjetGets, TradoObjet[] tradoObjetGives, string myCourriel, string hisCourriel)
		{
			InitializeComponent ();
		    MyCourriel = myCourriel;
            HisCourriel = hisCourriel;
		    tradoObjetGet = tradoObjetGets;
		    tradoObjetGive = tradoObjetGives;
		}

	    private static List<TradoObjet> liste = new List<TradoObjet>();
        protected override async void OnAppearing()
	    {
            base.OnAppearing();
	        liste = await Trado.serviceMobile.GetTable<TradoObjet>().ToListAsync();
            var resultat = liste.Where(x => x.CourrielUsager.ToUpper().Equals(HisCourriel.ToUpper())).ToList();
            ListView.ItemsSource = resultat;
	    }

        TradoObjet objetSelectionne = new TradoObjet();
	    private void Select_OnClicked(object sender, SelectedItemChangedEventArgs e)
	    {
	        objetSelectionne = (TradoObjet)e.SelectedItem;
            int count = tradoObjetGet.Length;
	        tradoObjetGive[count] = objetSelectionne;
            Navigation.PushAsync(new PageAjouterOffre(tradoObjetGet, tradoObjetGive, MyCourriel, HisCourriel));
	    }

	    private void Annule_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new PageAjouterOffre(tradoObjetGet, tradoObjetGive, MyCourriel, HisCourriel));
	    }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            TradoObjet itemToDelete = (sender as MenuItem).BindingContext as TradoObjet;
        }
    }
}