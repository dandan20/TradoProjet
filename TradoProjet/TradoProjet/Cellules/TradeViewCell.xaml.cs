using Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cellules
{ 

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TradeViewCell : Grid
	{
        private int buttonsConfig = 0;

        public static readonly BindableProperty CourrielProperty =
            BindableProperty.Create("Courriel", typeof(string), typeof(TradeViewCell), null);

        public string Courriel
        {
            get { return (string)GetValue(CourrielProperty); }
            set { SetValue(CourrielProperty, value); }
        }

        public static readonly BindableProperty ÉchangeProperty =
            BindableProperty.Create("Échange", typeof(TradoÉchange), typeof(TradeViewCell), null);

        public TradoÉchange Échange
        {
            get { return (TradoÉchange)GetValue(ÉchangeProperty); }
            set { SetValue(ÉchangeProperty, value); }
        }

        public TradeViewCell()
		{
			InitializeComponent ();

            NomInitial.Text = Échange.tradoObjetsGive[0].Nom;
            Nom2.Text = Échange.tradoObjetsGet[0].Nom;
            ImageInitial.Source = ImageSource.FromUri(Échange.tradoObjetsGive[0].FichierDeImage);
            Image2.Source = ImageSource.FromUri(Échange.tradoObjetsGet[0].FichierDeImage);
            int sizeGive = Échange.tradoObjetsGive.Length;
            int sizeGet = Échange.tradoObjetsGet.Length;
            if(sizeGive > 1)
            {
                ObjetsInitial.Text = "+ " + (sizeGive - 1).ToString() + " objets";
            } else
            {
                ObjetsInitial.Text = "";
            }
            if(sizeGet > 1)
            {
                Objets2.Text = "+ " + (sizeGet - 1).ToString() + " objets";
            } else
            {
                Objets2.Text = "";
            }
            if (Échange.setup == false)
            {
                ButtonSetUpOrConfirmOrModify.Text = "Mettre en place date, heure et lieu";
                ButtonDeleteOrModify.Text = "Supprimer l'echange";
                ButtonDelete.IsVisible = false;
                buttonsConfig = 0;
            }
            else
            {
                if (Échange.confirmed == true || Échange.confirm2 == true)
                {
                    ButtonSetUpOrConfirmOrModify.Text = "Modifier date, heure et lieu";
                    ButtonDeleteOrModify.Text = "Supprimer l'echange";
                    ButtonDelete.IsVisible = false;
                    buttonsConfig = 1;
                }
                else
                {
                    ButtonSetUpOrConfirmOrModify.Text = "Confirmer date, heure et lieu";
                    ButtonDeleteOrModify.Text = "Modifier date, heure et lieu";
                    ButtonDelete.Text = "Supprimer l'echange";
                    buttonsConfig = 2;
                }
            }
        }

        private void ButtonSetUpOrConfirmOrModify_Clicked(object sender, EventArgs e)
        {
            switch (buttonsConfig)
            {
                case 0:
                    Navigation.PushAsync(new PageSetUpDTL(Échange, Courriel));
                    break;
                case 1:
                    Navigation.PushAsync(new PageModifyDTL(Échange, Courriel));
                    break;
                case 2:
                    Échange.confirm2 = true;
                    break;
                default:
                    break;
            }
        }

        private void ButtonDeleteOrModify_Clicked(object sender, EventArgs e)
        {
            switch (buttonsConfig)
            {
                case 0:
                    Trado.serviceMobile.GetTable<TradoÉchange>().DeleteAsync(Échange);
                    break;
                case 1:
                    Trado.serviceMobile.GetTable<TradoÉchange>().DeleteAsync(Échange);
                    break;
                case 2:
                    Navigation.PushAsync(new PageModifyDTL(Échange, Courriel));
                    break;
                default:
                    break;
            }
        }

        private void ButtonDelete_Clicked(object sender, EventArgs e)
        {
            Trado.serviceMobile.GetTable<TradoÉchange>().DeleteAsync(Échange);
        }
    }
}