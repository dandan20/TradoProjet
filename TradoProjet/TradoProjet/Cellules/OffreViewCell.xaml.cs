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
	public partial class OffreViewCell : Grid
	{
        private bool IsMine;

        public static readonly BindableProperty CourrielProperty =
            BindableProperty.Create("Courriel", typeof(string), typeof(OffreViewCell), null);

        public string Courriel
        {
            get { return (string)GetValue(CourrielProperty); }
            set { SetValue(CourrielProperty, value); }
        }

        public static readonly BindableProperty offreProperty =
            BindableProperty.Create("offre", typeof(TradoÉchange), typeof(OffreViewCell), null);

        public TradoÉchange Offre
        {
            get { return (TradoÉchange)GetValue(offreProperty); }
            set { SetValue(offreProperty, value); }
        }

        public OffreViewCell ()
		{
			InitializeComponent ();

            NomInitial.Text = Offre.tradoObjetsGive[0].Nom;
            Nom2.Text = Offre.tradoObjetsGet[0].Nom;
            ImageInitial.Source = ImageSource.FromUri(Offre.tradoObjetsGive[0].FichierDeImage);
            Image2.Source = ImageSource.FromUri(Offre.tradoObjetsGet[0].FichierDeImage);
            int sizeGive = Offre.tradoObjetsGive.Length;
            int sizeGet = Offre.tradoObjetsGet.Length;
            if(Courriel == Offre.UsagerInitial.Courriel)
            {
                IsMine = true;
            } else if(Courriel == Offre.Usager2.Courriel)
            {
                IsMine = false;
            }
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
            if(IsMine == true)
            {
                ButtonAcceptOrCancel.Text = "Supprimer";
                ButtonDeclineorModify.Text = "Modifier";
                ButtonModify.IsVisible = false;
            } else
            {
                ButtonAcceptOrCancel.Text = "Accepter";
                ButtonDeclineorModify.Text = "Refuser";
                ButtonModify.IsVisible = true;
            }
		}

        private void ButtonAcceptOrCancel_Clicked(object sender, EventArgs e)
        {
            if(IsMine == true)
            {
                //Delete
            } else
            {
                Offre.acceptation2 = true;
            }
        }

        private void ButtonDeclineorModify_Clicked(object sender, EventArgs e)
        {
            if(IsMine == true)
            {
                Navigation.PushAsync(new PageModifierOffre(Courriel, Offre));
            } else
            {
                //Decline = offer goes into failed and declined
            }
        }

        private void ButtonModify_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageModifierOffre(Courriel, Offre));
        }
    }
}