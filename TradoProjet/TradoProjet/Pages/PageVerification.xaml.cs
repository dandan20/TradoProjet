using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageVerification : ContentPage
	{
        public TradoUsager TradoUsagers;
		public PageVerification (TradoUsager tradoUsager)
		{
			InitializeComponent ();
		}

        private void GoButton_Clicked(object sender, EventArgs e)
        {
            if(CodeOrNo.Text == TradoUsagers.code.ToString())
            {
                TradoUsagers.verifie = true;
                CodeOrNoErreur.Text = "";
                DisplayAlert("Succès", "Vous pouvez maintenant accéder à Trado", "Ok");
                Navigation.PushAsync(new PagePrincipale());
            } else
            {
                CodeOrNoErreur.Text = "Le code n'est pas correct.";
            }
        }
    }
}