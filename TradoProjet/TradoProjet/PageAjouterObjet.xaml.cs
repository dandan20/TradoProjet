//Des bibliothèques d'informations pour insérer une/des images pour l'objet à ajouter.
using Plugin.Media;
using Plugin.Media.Abstractions;

//Utilisation d'une base de données locale pour pouvoir voir les objets sans Internet ou pour utiliser moins l'Internet.
using SQLite;

//Utilisation des models, dans ce cas-ci, le mode d'objet.
using TradoProjet.Model;

//Des bibliothèques d'informations assez fréquentes.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TradoProjet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAjouterObjet : ContentPage
    {
        public PageAjouterObjet()
        {
            InitializeComponent();
        }

        //Ceci est une fonction pré-déterminée qui fonctionne seulement quand le bouton d'ajout d'objet est cliqué.
        private void AjouterObjetOuServiceButton_Clicked(object sender, EventArgs e)
        {
            //création d'un nouvel objet qu'on nomme tradoObjet dans l'application
            TradoObjet tradoObjet = new TradoObjet
            {
                //nom de l'objet donné par le texte de l'entrée
                nom = NomObjetEntry.Text,
                //catégorie de l'objet donné par un sélectionneur de catégories
                catégorie = CategoriePicker.Title,
                //état de l'objet
                état = ÉtatPicker.Title,
                //détails de l'objet
                détails = DetailsEntry.Text
            };

            //utilisation d'une base de données locale SQLite
            using (SQLiteConnection conn = new SQLiteConnection(Trado.emplacementDeBaseDeDonnées))
            {
                //création d'une table de TradoObjets
                conn.CreateTable<TradoObjet>();

                //Insertion d'un objet dans un int (nombre entier)
                int rangées = conn.Insert(tradoObjet);

                //Si (le nombre de rangées est plus grand que 0)
                if (rangées > 0)
                {
                    //Succès
                    DisplayAlert("Succès", "L'objet a été inséré par succès!", "Ok");
                }
                //Sinon
                else
                {
                    //Échec
                    DisplayAlert("Échec", "L'objet n'a pas pu etre inséré!", "Ok");
                }
            }
        }

        //Fonction async est une fonction qui doit attendre pour que la commande antérieure soit faite avant de faire la prochaine commande.
        //Cette fonction aide à choisir une image.
        private async void ChoisirImageButton_Clicked(object sender, EventArgs e)
        {
            //Initialiser les fonctions médiatiques
            await CrossMedia.Current.Initialize();

            //Si on ne peut pas utiliser la fonctionnalité de choix de photo
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                //erreur
                await DisplayAlert("Erreur", "Cette fonctionnalité n'est pas disponible sur votre téléphone", "Ok");
                //retourné vers la fonction async
                return;
            }

            var optionsDeMédia = new PickMediaOptions()
            {
                //Image sera de taille médium
                PhotoSize = PhotoSize.Medium
            };

            //L'image sélectionné est mise dans une variable nommée imageSélectionné
            var imageSélectionné = await CrossMedia.Current.PickPhotoAsync(optionsDeMédia);

            //Si l'imageSélectionné est nulle
            if(imageSélectionné == null)
            {
                //Erreur
                await DisplayAlert("Erreur", "Il y a eu une erreur d'obtention de votre image", "Ok");
                return;
            }

            //L'image change à l'imageSélectionné
            ObjetImage.Source = ImageSource.FromStream(() => imageSélectionné.GetStream());
        }
    }
}