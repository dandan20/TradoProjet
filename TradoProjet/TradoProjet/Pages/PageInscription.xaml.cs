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
using System.Security.Cryptography;
using Pages;

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
            var nomUsager = NomUsagerEntry.Text;
            var courriel = CourrielEntry.Text;
            var prenom = PrenomEntry.Text;
            var motdepasse = MotDePasseEntry.Text;
            var confirmeMotdepasse = ConfirmerMotDePasseEntry.Text;
            var nomUsagerNull = string.IsNullOrWhiteSpace(nomUsager);
            var courrielNull = string.IsNullOrWhiteSpace(courriel);
            var motdepasseNull = string.IsNullOrWhiteSpace(motdepasse);
            var prenomNull = string.IsNullOrWhiteSpace(prenom);
            var confirmeMotdepasseNull = string.IsNullOrWhiteSpace(confirmeMotdepasse);
            var courrielPattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
            var nomUsagerPattern = "^[^\\W](?=.*[a-z]).{4,14}$";
            var motdepassePattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d).{5,14}$";
            bool isCourriel = Regex.IsMatch(courriel, courrielPattern);
            bool isNomUsager = Regex.IsMatch(nomUsager, nomUsagerPattern);
            var isCCI = courriel.EndsWith("@ccharlemagne.com");
            bool isMotdepasse = Regex.IsMatch(motdepasse, motdepassePattern);
            bool isNomUsagerCorrect = false;
            bool isMotdepasseCorrect = false;
            bool isConfirmeMotdepasseCorrect = false;
            bool isCourrielCorrect = false;
            bool isPrenomCorrect = false;

            //On essaille de trouver le même courriel que celui qui veut être inscrit dans la base de données pour ne pas créer deux usagers avec le même 
            //courriel et on le met dans une variable nommé usager.
            var usager = (await Trado.serviceMobile.GetTable<TradoUsager>().Where(u => u.Courriel == CourrielEntry.Text).ToListAsync()).FirstOrDefault();
            var nomDusager = (await Trado.serviceMobile.GetTable<TradoUsager>().Where(n => n.NomDUsager == NomUsagerEntry.Text).ToListAsync());

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
            } else
            {
                isPrenomCorrect = true;
                PrenomEntryErreurLabel.Text = "";
            }

            if (nomUsagerNull == true)
            {
                NomUsagerEntryErreurLabel.Text = "Ton nom d'usager est nécessaire.";
            }
            else if (isNomUsager == false)
            {
                NomUsagerEntryErreurLabel.Text = "Ton nom d'usager doit contenir entre 5 et 15 charactères.";
            }
            else if (nomDusager != null)
            {
                NomUsagerEntryErreurLabel.Text = "Ce nom d'usager a déjà été pris.";
            } else
            {
                isNomUsagerCorrect = true;
                NomUsagerEntryErreurLabel.Text = "";
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

            if(isCourrielCorrect == true && isNomUsagerCorrect == true && isMotdepasseCorrect == true && isPrenomCorrect == true && isConfirmeMotdepasseCorrect == true)
            {
                Random rnd = new Random();
                int verifCode = rnd.Next(1000, 10000);

                //Si le mot de passe est égal au mot de passe à confirmer
                //On peut inscrire l'usager
                TradoUsager tradoUsager = new TradoUsager()
                {
                    Courriel = CourrielEntry.Text,
                    MotDePasse = MotDePasseEntry.Text,
                    Nom = PrenomEntry.Text,
                    NomDUsager = NomUsagerEntry.Text,
                    code = verifCode
                };

                await Trado.serviceMobile.GetTable<TradoUsager>().InsertAsync(tradoUsager);

                var apiKey = Environment.GetEnvironmentVariable("TradoAPIKeySENDGRID");
                var client = new SendGridClient(apiKey);

                await DisplayAlert("Succès", "Ton inscription a été faite. Vérifie ton courriel!", "Ok");

                var msg = new SendGridMessage();

                msg.SetFrom(new EmailAddress("tradoapp@gmail.com", "Trado"));

                var recipient = new EmailAddress(courriel, prenom);

                msg.AddTo(recipient);

                msg.SetSubject("Vérification de Courriel pour Trado");

                msg.AddContent(MimeType.Html, "<h1>Bienvenue à Trado!</h1>");
                msg.AddContent(MimeType.Text, "Merci pour votre inscription, "+ PrenomEntry.Text +"! Copier ce code dans l'application pour valider votre courriel: " + verifCode.ToString());

                var response = await client.SendEmailAsync(msg);

                await Navigation.PushAsync(new PageVerification(tradoUsager));
            };

        }

        private void VerificationButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}
