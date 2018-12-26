using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet.Cellules;
using TradoProjet.Model;
using TradoProjet.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TradoProjet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageAjouterOffre : ContentPage
	{
        public TradoObjet[] tradoObjetGet;
        public TradoObjet[] tradoObjetGive;
	    public string Courriel;
		public PageAjouterOffre (TradoObjet[] tradoObjetsGets, TradoObjet[] tradoObjetGives, string courriel)
		{
			InitializeComponent ();
		    Courriel = courriel;
            int countGet = 0;
		    foreach (var tradoObjet in tradoObjetsGets)
		    {
		        tradoObjetGet[countGet] = tradoObjet;
                countGet++;
		    }

            int countGive = 0;
            foreach (var tradoObjet in tradoObjetGives)
            {
                tradoObjetGet[countGive] = tradoObjet;
                countGive++;
            }

		    GetList.ItemsSource = tradoObjetsGets;
            GiveList.ItemsSource = tradoObjetGives;
        }
        
        private void AddGetObjectButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageChoisirGiveObjet(tradoObjetGet, tradoObjetGive, Courriel));
        }

        private void AddGiveObjectButton_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new PageChoisirGetObjet(tradoObjetGet, tradoObjetGive, Courriel));
	    }
	}
}