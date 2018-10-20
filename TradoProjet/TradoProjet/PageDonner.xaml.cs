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
    public partial class PageDonner : ContentPage
    {
        public PageDonner()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(Trado.DatabaseLocation))
            {
                conn.CreateTable<TradoObject>();
                var tradoObjects = conn.Table<TradoObject>().ToList();
                ObjetsListView.ItemsSource = tradoObjects;
            }
        }

        private void AjouterObjetButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageAjouterObjet());
        }
    }
}