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

            var assembly = typeof(PagePrincipale);

            TradoLogoImage.Source = ImageSource.FromResource("TradoProjet.Assets.Images.TradoLogoImage.png", assembly);
        }

        private async void AccederButton_Clicked(object sender, EventArgs e)
        {

            bool isEmailEmpty = string.IsNullOrEmpty(CourrielEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(MotDePasseEntry.Text);

            if(isEmailEmpty || isPasswordEmpty)
            {

            } else
            {
                var tradoUser = (await Trado.MobileService.GetTable<TradoUser>().Where(u => u.Email == CourrielEntry.Text).ToListAsync()).FirstOrDefault();

                if(tradoUser != null)
                {
                    if(tradoUser.Password == MotDePasseEntry.Text)
                    {
                        await Navigation.PushAsync(new PageMaison());
                    } else
                    {
                        await DisplayAlert("Error", "Email or password are incorrect", "Ok");
                    }
                } else
                {
                    await DisplayAlert("Error", "There was an error logging you in", "Ok");
                }
            }
        }

        private void InscriptionButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageInscription());
        }

        private void GoButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageMaison());
        }
    }
}
