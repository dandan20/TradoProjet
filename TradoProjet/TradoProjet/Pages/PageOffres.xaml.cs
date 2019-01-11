using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradoProjet;
using TradoProjet.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PageOffres : ContentPage
	{
        public string MyCourriel;
		public PageOffres (string courriel)
		{
            InitializeComponent ();
            MyCourriel = courriel;
        }

        List<TradoUsager> userList;
        List<TradoÉchange> offreList;
        TradoUsager me;
        List<TradoÉchange> offreMoi;
        List<TradoÉchange> offreMe;
        List<TradoÉchange> offreToi;
        List<TradoÉchange> offreYou;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            userList = await Trado.serviceMobile.GetTable<TradoUsager>().ToListAsync();
            offreList = await Trado.serviceMobile.GetTable<TradoÉchange>().ToListAsync();
            me = userList.Where(X => X.Courriel.ToUpper().Equals(MyCourriel.ToUpper())).Single();
            offreMoi = offreList.Where(X => X.UsagerInitial.Equals(me)).ToList();
            offreMe = offreMoi.Where(x => x.acceptation.Equals(false)).ToList();
            offreToi = offreList.Where(x => x.Usager2.Equals(me)).ToList();
            offreYou = offreToi.Where(x => x.acceptation.Equals(false)).ToList();
            OfferList.ItemsSource = offreMe;
        }

        private void MesOffresButton_Clicked(object sender, EventArgs e)
        {
            OfferList.ItemsSource = offreMe;
        }

        private void LeursOffresButton_Clicked(object sender, EventArgs e)
        {
            OfferList.ItemsSource = offreYou;
        }
    }
}