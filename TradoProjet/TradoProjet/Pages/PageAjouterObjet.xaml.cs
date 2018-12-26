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
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

//Des bibliothèques pour le stockage Azure
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.Storage.Auth;

namespace TradoProjet
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAjouterObjet : ContentPage
    {
        public string Courriel;
        public PageAjouterObjet(string courriel)
        {
            InitializeComponent();

            Courriel = courriel;

            string[] catégories = new string[] { "Livres", "Jeux Vidéos", "Musique", "Films", "Techno", "Maison", "Jardin", "Animaux", "Outils", "Nourriture", "Santé et beauté", "Jouets", "Vêtements", "Chaussures", "Bijoux", "Montres", "Sports", "Plein Air", };

            string[] états = new string[] { "Nouveau", "Utilisé", "Mauvais état", "Pour parties seulement" };

            CategoriePicker.ItemsSource = catégories.ToList();

            ÉtatPicker.ItemsSource = états.ToList();
        }

        //Ceci est une fonction pré-déterminée qui fonctionne seulement quand le bouton d'ajout d'objet est cliqué.
        private async void AjouterObjetButton_Clicked(object sender, EventArgs e)
        {
            var objet = (await Trado.serviceMobile.GetTable<TradoObjet>().Where(u => u.Nom == NomObjetEntry.Text).ToListAsync()).FirstOrDefault();

            //création d'un nouvel objet qu'on nomme tradoObjet dans l'application
            TradoObjet tradoObjet = new TradoObjet
            {
                //nom de l'objet donné par le texte de l'entrée
                Nom = NomObjetEntry.Text,
                //catégorie de l'objet donné par un sélectionneur de catégories
                Categorie = selectedCat,
                //état de l'objet
                Etat = selectedEtat,
                //détails de l'objet
                Details = DetailsEntry.Text,
                //courriel de l'usager avec l'objet
                CourrielUsager = Courriel
            };

            await Trado.serviceMobile.GetTable<TradoObjet>().InsertAsync(tradoObjet);

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
                    await DisplayAlert("Succès", "L'objet a été inséré par succès!", "Ok");
                }
                //Sinon
                else
                {
                    //Échec
                    await DisplayAlert("Échec", "L'objet n'a pas pu etre inséré!", "Ok");
                }
            }
        }
    /*else if (objet != null)
    {
        //Échec
        await DisplayAlert("Échec", "Cet objet a déjà été utilisé.", "Ok");
    }*/

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

            // Éxécuter la fonction suivante avec l'image sélectionné comme paramètre
            TéléchargerImageVersUnServeur(imageSélectionné.GetStream());
        }

        //Cette fonction télécharge l'image vers un serveur
        private async void TéléchargerImageVersUnServeur(Stream stream)
        {
            var account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=trado;AccountKey=SnMiml4S5hwVqW9FJF6ZM51vakUUrhP2dDYZDU3oAo7BOpUin+q6eDQww7etuUmDa+F1N7B9TPP6PZnT24WkXw==;EndpointSuffix=core.windows.net");
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("recipientimages");
            await container.CreateIfNotExistsAsync();

            var name = Guid.NewGuid().ToString();
            var blockBlob = container.GetBlockBlobReference($"{name}.jpg");
            await blockBlob.UploadFromStreamAsync(stream);

            string url = blockBlob.Uri.OriginalString;
        }

        public string selectedCat;
        private void CategoriePicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            selectedCat = picker.SelectedItem.ToString();
        }

        public string selectedEtat;
        private void EtatPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            selectedEtat = picker.SelectedItem.ToString();
        }
    }
}
