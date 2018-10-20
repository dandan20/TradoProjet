using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet.Model;
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

        private void AjouterObjetOuServiceButton_Clicked(object sender, EventArgs e)
        {
            TradoObject tradoObjet = new TradoObject
            {
                name = NomObjetEntry.Text,
                category = CategoriePicker.Title,
                state = StatusPicker.Title,
                details = DetailsEntry.Text
            };

            using (SQLiteConnection conn = new SQLiteConnection(Trado.DatabaseLocation))
            {
                conn.CreateTable<TradoObject>();
                int rows = conn.Insert(tradoObjet);

                if (rows > 0)
                {
                    DisplayAlert("Success", "Object succesfully inserted!", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Object failed to be inserted!", "Ok");
                }
            }
        }

        private async void ChoisirImageButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Error", "This is not supported on your device", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions()
            {
                PhotoSize = PhotoSize.Medium
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if(selectedImageFile == null)
            {
                await DisplayAlert("Error", "There was an error when trying to get your image", "Ok");
                return;
            }

            ObjetImage.Source = ImageSource.FromStream(() => selectedImageFile.GetStream());
        }
    }
}