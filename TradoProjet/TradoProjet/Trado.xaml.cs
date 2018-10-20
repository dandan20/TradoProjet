//using Microsoft.WindowsAzure.MobileServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TradoProjet
{
    public partial class Trado : Application
    {
        public static string DatabaseLocation = string.Empty;
        public static MobileServiceClient MobileService = new MobileServiceClient("https://trado.azurewebsites.net");

        public Trado(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PagePrincipale());

            DatabaseLocation = databaseLocation;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
