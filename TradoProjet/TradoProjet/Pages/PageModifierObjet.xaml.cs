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
	public partial class PageModifierObjet : ContentPage
	{
        TradoObjet tradoObjet = new TradoObjet();

		public PageModifierObjet (TradoObjet objetAmodifier)
		{
			InitializeComponent ();

            tradoObjet = objetAmodifier;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = tradoObjet;
        }

        private async void ModifierObjetButton_Clicked(object sender, EventArgs e)
        {
            await Trado.serviceMobile.GetTable<TradoObjet>().UpdateAsync(tradoObjet);
        }
    }
}