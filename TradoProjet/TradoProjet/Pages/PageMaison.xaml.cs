using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradoProjet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageMaison : ContentPage
	{
	    public string Courriel;
		public PageMaison (string courriel)
		{
			InitializeComponent ();
		    Courriel = courriel;
		}

        //Bouton temporaire pour se rendre à la page donner
        private void DonnerButton_Clicked(object sender, EventArgs e)
        {
            //Navigation à la page donner
            Navigation.PushAsync(new PageDonner(Courriel));
        }

        private void RechercheButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageRecherche(Courriel));
        }
    }
}