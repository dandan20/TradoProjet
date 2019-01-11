using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradoProjet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageObjet : ContentPage
	{
        TradoObjet tradoObjet = new TradoObjet();
	    public TradoObjet[] tradoObjetGet;
	    public TradoObjet[] tradoObjetGive;
	    public string HisCourriel;
        public string MyCourriel;
		public PageObjet (TradoObjet objet, string myCourriel)
		{
			InitializeComponent ();
            tradoObjet = objet;
            HisCourriel = tradoObjet.CourrielUsager;
		    tradoObjetGet[0] = objet;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = tradoObjet;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageAjouterOffre(tradoObjetGet, tradoObjetGive, MyCourriel, HisCourriel));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}