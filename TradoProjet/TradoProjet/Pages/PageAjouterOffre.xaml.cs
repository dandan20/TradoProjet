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
	    public string MyCourriel;
        public string HisCourriel;
		public PageAjouterOffre (TradoObjet[] tradoObjetsGets, TradoObjet[] tradoObjetGives, string myCourriel, string hisCourriel)
		{
			InitializeComponent ();
		    MyCourriel = myCourriel;
            HisCourriel = hisCourriel;
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
            Navigation.PushAsync(new PageChoisirGiveObjet(tradoObjetGet, tradoObjetGive, MyCourriel, HisCourriel));
        }

        private void AddGiveObjectButton_OnClicked(object sender, EventArgs e)
	    {
	        Navigation.PushAsync(new PageChoisirGetObjet(tradoObjetGet, tradoObjetGive, MyCourriel, HisCourriel));
	    }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var userList = await Trado.serviceMobile.GetTable<TradoUsager>().ToListAsync();
            TradoUsager userInitial = userList.Where(X => X.Courriel.ToUpper().Equals(MyCourriel.ToUpper())).Single();
            TradoUsager user2 = userList.Where(x => x.Courriel.ToUpper().Equals(HisCourriel.ToUpper())).Single();
            if (tradoObjetGet.Length >= 1 && tradoObjetGive.Length >= 1)
            {
                TradoÉchange tradoÉchange = new TradoÉchange
                {
                    tradoObjetsGet = tradoObjetGet,
                    tradoObjetsGive = tradoObjetGive,
                    UsagerInitial = userInitial,
                    Usager2 = user2,
                    acceptationInitial = true,
                    acceptation2 = false,
                    acceptation = false
                };

                await Trado.serviceMobile.GetTable<TradoÉchange>().InsertAsync(tradoÉchange);
            } else if (tradoObjetGet.Length < 1)
            {
                var reponse = await DisplayAlert("Erreur", "Est-ce que tu veux donner un objet sans en recevoir un?", "Oui", "Non");
                if(reponse == true)
                {
                    TradoÉchange tradoÉchange = new TradoÉchange
                    {
                        tradoObjetsGet = tradoObjetGet,
                        tradoObjetsGive = tradoObjetGive,
                        UsagerInitial = userInitial,
                        Usager2 = user2,
                        acceptationInitial = true,
                        acceptation2 = false
                    };

                    await Trado.serviceMobile.GetTable<TradoÉchange>().InsertAsync(tradoÉchange);
                }
            } else if (tradoObjetGive.Length < 1)
            {
                await DisplayAlert("Erreur", "Tu ne peux pas faire une offre sans donner quelque chose.", "Ok");
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            TradoObjet itemToDelete = (sender as MenuItem).BindingContext as TradoObjet;
        }
    }
}