using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using TradoProjet.Model;

namespace TradoProjet
{
    public partial class PagePrincipale : ContentPage
    {
        public PagePrincipale()
        {
            InitializeComponent();

            //type de page (page principale) va dans variable assemblé
            var assemblé = typeof(PagePrincipale);

            //source de l'image du logo complet de mon application
            TradoLogoImage.Source = ImageSource.FromResource("TradoProjet.Assets.Images.TradoLogoImage.png", assemblé);
        }

        //bouton accéder fonction
        private async void AccederButton_Clicked(object sender, EventArgs e)
        {

            /*Ceci est une booléenne. Ou elle est vrai ou elle est fausse. Cette 
            booléenne représente si le courriel est vide.*/
            bool courrielVide = string.IsNullOrEmpty(CourrielEntry.Text);
            //Cette booléenne représente si le mot de passe est vide.
            bool motDePasseVide = string.IsNullOrEmpty(MotDePasseEntry.Text);

            //Si le courriel et le mot de passe sont vide
            if(courrielVide || motDePasseVide)
            {
                //faire ça (rien pour le moment)
            }
            //Sinon
            else
            {
                //On trouve le premier courriel qui correspond à l'entrée du courriel et on met le courriel dans la variable tradoUsager.
                var tradoUsager = (await Trado.serviceMobile.GetTable<TradoUsager>().Where(u => u.Courriel == CourrielEntry.Text).ToListAsync()).FirstOrDefault();

                //Si un courriel correspond (non-nul)
                if(tradoUsager != null)
                {
                    //Si le mot de passe de l'usager trouvé est égal au mot de passe dans l'entrée
                    if(tradoUsager.MotDePasse == MotDePasseEntry.Text)
                    {
                        //Naviguer vers la PageMaison
                        await Navigation.PushAsync(new PageMaison());
                    } else
                    {
                        //Erreur
                        await DisplayAlert("Erreur", "Courriel ou mot de passe incorrect", "Ok");
                    }
                }
                //Sinon
                else
                {
                    //Erreur
                    await DisplayAlert("Erreur", "Il y a eu une erreur lors de votre identification", "Ok");
                }
            }
        }

        //Bouton s'inscrire
        private void InscriptionButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageInscription());
        }

        //Bouton go (accès temporairement sans inscription pour raisons autres)
        private void GoButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageMaison());
        }
    }
}
