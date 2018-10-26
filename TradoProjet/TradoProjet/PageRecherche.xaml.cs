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
	public partial class PageRecherche : ContentPage
	{
		public PageRecherche ()
		{
			InitializeComponent ();
		}

        //Fonction activé quand la recherche est faite.
        private void RechercheSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            //Rechercher
        }
    }
}