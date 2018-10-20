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

        private async void InscriptionButton_Clicked(object sender, EventArgs e)
        {
            if(MotDePasseEntry.Text == ConfirmerMotDePasseEntry.Text)
            {
                //On peut inscrire l'usager
                TradoUser tradoUser = new TradoUser
                {
                    Email = CourrielEntry.Text,
                    Password = MotDePasseEntry.Text
                };

                var user = (await Trado.MobileService.GetTable<TradoUser>().Where(u => u.Email == CourrielEntry.Text).ToListAsync()).FirstOrDefault();

                if (user == null) {
                    await Trado.MobileService.GetTable<TradoUser>().InsertAsync(tradoUser);
                } else if (user != null)
                {
                    await DisplayAlert("Error", "This email has already been used", "Ok");
                }
            } else
            {
                await DisplayAlert("Error", "Passwords don't match", "Ok");
            }
        }

    }
}