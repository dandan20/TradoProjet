using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet;
using TradoProjet.Model;
using TradoProjet.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageModifierOffre : ContentPage
	{
        public TradoObjet[] tradoGetObjets;
        public TradoObjet[] tradoGiveObjets;
        public TradoÉchange tradoÉchanges;
        public string MyCourriel;
        public string HisCourriel;
        public bool isMine;
		public PageModifierOffre (string myCourriel, TradoÉchange tradoÉchange)
		{
			InitializeComponent ();
            tradoÉchanges = tradoÉchange;
            MyCourriel = myCourriel;
            if(MyCourriel == tradoÉchanges.UsagerInitial.Courriel)
            {
                isMine = true;
                SendOrSave.Text = "Save";
                tradoGetObjets = tradoÉchanges.tradoObjetsGet;
                tradoGiveObjets = tradoÉchanges.tradoObjetsGive;
                HisCourriel = tradoÉchanges.Usager2.Courriel;
            } else if (MyCourriel == tradoÉchanges.Usager2.Courriel)
            {
                isMine = false;
                SendOrSave.Text = "Send";
                tradoGetObjets = tradoÉchanges.tradoObjetsGive;
                tradoGiveObjets = tradoÉchanges.tradoObjetsGet;
                HisCourriel = tradoÉchanges.UsagerInitial.Courriel;
            }
		}

        private void ToolbarItem_Clicked()
        {
            //if isMine save else send
            if(isMine == true)
            {
                TradoÉchange tradoÉchange = new TradoÉchange
                {
                    tradoObjetsGet = tradoGetObjets,
                    tradoObjetsGive = tradoGiveObjets,
                    UsagerInitial = tradoÉchanges.UsagerInitial,
                    Usager2 = tradoÉchanges.Usager2,
                    acceptationInitial = true,
                    acceptation2 = false
                };

                Trado.serviceMobile.GetTable<TradoÉchange>().UpdateAsync(tradoÉchange);
            } else
            {
                TradoÉchange tradoÉchange = new TradoÉchange
                {
                    tradoObjetsGet = tradoGetObjets,
                    tradoObjetsGive = tradoGiveObjets,
                    UsagerInitial = tradoÉchanges.Usager2,
                    Usager2 = tradoÉchanges.UsagerInitial,
                    acceptationInitial = false,
                    acceptation2 = true
                };

                Trado.serviceMobile.GetTable<TradoÉchange>().UpdateAsync(tradoÉchange);
            }
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            TradoObjet itemToDelete = (sender as MenuItem).BindingContext as TradoObjet;
        }

        private void AddGetObjectButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageChoisirGetObjet(tradoGetObjets, tradoGiveObjets, MyCourriel, HisCourriel));
        }

        private void AddGiveObjectButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageChoisirGetObjet(tradoGetObjets, tradoGiveObjets, MyCourriel, HisCourriel));
        }
    }
}