using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;
using System.Text.RegularExpressions;
using SendGrid;
using SendGrid.Helpers.Mail;

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
            //only gets till here WTF!?
            var courriel = CourrielEntry.Text;
            var prenom = PrenomEntry.Text;
            var motdepasse = MotDePasseEntry.Text;
            var confirmeMotdepasse = ConfirmerMotDePasseEntry.Text;
            var courrielNull = string.IsNullOrWhiteSpace(courriel);
            var motdepasseNull = string.IsNullOrWhiteSpace(motdepasse);
            var prenomNull = string.IsNullOrWhiteSpace(prenom);
            var confirmeMotdepasseNull = string.IsNullOrWhiteSpace(confirmeMotdepasse);
            var courrielPattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
            bool isCourriel = Regex.IsMatch(courriel, courrielPattern);
            var isCCI = courriel.EndsWith("@ccharlemagne.com");
            var motdepassePattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6,15}$";
            var prenomPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{6,15}$";
            bool isMotdepasse = Regex.IsMatch(motdepasse, motdepassePattern);
            bool isPrenom = Regex.IsMatch(prenom, prenomPattern);
            bool isMotdepasseCorrect = false;
            bool isConfirmeMotdepasseCorrect = false;
            bool isCourrielCorrect = false;
            bool isPrenomCorrect = false;

            //On essaille de trouver le même courriel que celui qui veut être inscrit dans la base de données pour ne pas créer deux usagers avec le même 
            //courriel et on le met dans une variable nommé usager.
            var usager = (await Trado.serviceMobile.GetTable<TradoUsager>().Where(u => u.Courriel == CourrielEntry.Text).ToListAsync()).FirstOrDefault();

            if (courrielNull == true)
            {
                CourrielEntryErreurLabel.Text = "Un courriel es nécessaire.";
            } else if (isCourriel == false || isCCI == false)
            {
                CourrielEntryErreurLabel.Text = "Ce courriel ne respecte pas un format de courriel. Le courriel doit terminer avec @ccharlemagne.com";
            } else if (usager == null)
            {
                isCourrielCorrect = true;
                CourrielEntryErreurLabel.Text = "";
            } else if(usager != null)
            {
                CourrielEntryErreurLabel.Text = "Ce courriel a déjà été utilisé.";
            }

            if(prenomNull == true)
            {
                PrenomEntryErreurLabel.Text = "Ton prénom est necessaire pour les communications par courriel.";
            } else if (isPrenom == false)
            {
                PrenomEntryErreurLabel.Text = "Ceci n'est pas un prénom.";
            } else
            {
                isPrenomCorrect = true;
                PrenomEntryErreurLabel.Text = "";
            }

            if (motdepasseNull == true)
            {
                MotDePasseEntryErreurLabel.Text = "Un mot de passe est nécessaire.";
            } else if (isMotdepasse == false)
            {
                MotDePasseEntryErreurLabel.Text = "Le mot de passe doit avoir entre 6 et 15 charactères. Une lettre majuscule, une lettre minuscule et un chiffre est au minimum.";
            } else
            {
                isMotdepasseCorrect = true;
                MotDePasseEntryErreurLabel.Text = "";
            }

            if (isMotdepasseCorrect == true)
            {
                if (confirmeMotdepasseNull == true)
                {
                    ConfirmerMotDePasseEntryErreurLabel.Text = "Reécris ton mot de passe pour le confirmer.";
                } else if (confirmeMotdepasse == motdepasse)
                {
                    isConfirmeMotdepasseCorrect = true;
                    ConfirmerMotDePasseEntryErreurLabel.Text = "";
                }
            }

            if(isCourrielCorrect == true && isMotdepasseCorrect == true && isPrenomCorrect == true && isConfirmeMotdepasseCorrect == true)
            {
                //Si le mot de passe est égal au mot de passe à confirmer
                //On peut inscrire l'usager
                TradoUsager tradoUsager = new TradoUsager()
                {
                    Courriel = CourrielEntry.Text,
                    MotDePasse = MotDePasseEntry.Text
                };

                await Trado.serviceMobile.GetTable<TradoUsager>().InsertAsync(tradoUsager);

                await DisplayAlert("Succès", "Ton inscription a été faite. Vérifie ton courriel!", "Ok");

                /*var msg = new SendGridMessage();

                msg.SetFrom(new EmailAddress("tradoapp@gmail.com", "Trado"));

                var recipient = new EmailAddress(courriel, prenom);

                msg.AddTo(recipient);

                msg.SetSubject("Vérification de Courriel pour Trado");

                msg.*/
            }

        }
    }
}
