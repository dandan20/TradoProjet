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
	public partial class PageEchanges : ContentPage
	{
        public string Courriel;
		public PageEchanges (string courriel)
		{
			InitializeComponent ();
            Courriel = courriel; 
		}

        List<TradoUsager> userList;
        List<TradoÉchange> echangeList;
        TradoUsager me;
        List<TradoÉchange> echanges;
        List<TradoÉchange> echangesTermine;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            userList = await Trado.serviceMobile.GetTable<TradoUsager>().ToListAsync();
            echangeList = await Trado.serviceMobile.GetTable<TradoÉchange>().ToListAsync();
            me = userList.Where(x => x.Courriel.ToUpper().Equals(Courriel.ToUpper())).Single();
            echanges = echangeList.Where(x => x.Usager2.Equals(me) || x.UsagerInitial.Equals(me)).ToList();
            echangesTermine = echanges.Where(x => x.termine.Equals(false)).ToList();
            EchangeList.ItemsSource = echanges;
        }

        private void EchangesButton_Clicked(object sender, EventArgs e)
        {
            EchangeList.ItemsSource = echanges;
        }

        private void EchangesTermineButton_Clicked(object sender, EventArgs e)
        {
            EchangeList.ItemsSource = echangesTermine;
        }
    }
}