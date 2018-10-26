using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;


namespace TradoProjet
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageInscription : ContentPage
	{

		public PageInscription ()
		{
			InitializeComponent ();
		}

        //Une fonction qui marche quand le bouton d'inscription a été pesé
        private async void InscriptionButton_Clicked(object sender, EventArgs e)
        {
            //Si le mot de passe est égal au mot de passe à confirmer
            if(MotDePasseEntry.Text == ConfirmerMotDePasseEntry.Text)
            {
                //On peut inscrire l'usager
                TradoUsager tradoUsager = new TradoUsager
                {
                    Courriel = CourrielEntry.Text,
                    MotDePasse = MotDePasseEntry.Text
                };

                //On essaille de trouver le même courriel que celui qui veut être inscrit dans la base de données pour ne pas créer deux usagers avec le même 
                //courriel et on le met dans une variable nommé usager.
                var usager = (await Trado.serviceMobile.GetTable<TradoUsager>().Where(u => u.Courriel == CourrielEntry.Text).ToListAsync()).FirstOrDefault();

                //Si un usager avec ce courriel est non-existant
                if (usager == null) {
                    //On peut insérer ce nouveau usager.
                    await Trado.serviceMobile.GetTable<TradoUsager>().InsertAsync(tradoUsager);
                }
                //Si un usager avec un courriel déjà utilisé existe
                else if (usager != null)
                {
                    await DisplayAlert("Erreur", "Ce courriel a déjà été utilisé", "Ok");
                }
            } else
            {
                await DisplayAlert("Erreur", "Les mots de passe ne sont pas les mêmes", "Ok");
            }
        }

    }
}