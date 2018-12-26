/*Ceci est un commentaire. J'utilise les commentaires pour que,
vous, les humains compreniez ce que signifie ma programmation.*/
//Ceci est un commentaire pour une ligne seulement.

/*Voici des bibliothèques d'information utiles presque à chaque fois. Je peux utiliser
des commandes définies dans ces bibliothèques pour faire beaucoup de choses.*/
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//Ceci est une bibliothèque utile pour Windows Azure.
using Microsoft.WindowsAzure.MobileServices;
using Algolia.Search;
using DLToolkit.Forms.Controls;

//Ceci est l'activation de la compilation XAML (langue de programmation pour ce qu'on voit à l'écran) au niveau de l'assemblage.
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

//Cette page est dans l'espace de noms de TradoProjet.
namespace TradoProjet
{
    /* Le type de classe (un modèle de code de programme extensible pour la
    création d'objets) est une application et le nom de la classe est Trado. */
    public partial class Trado : Application
    {
        /* Ceci est une variable accessible par d'autres page dans le projet (public/publique), de type string (texte)
        qui prendra l'emplacement de la base de données locale de l'usager. Elle est vide (Empty) au commencement de 
        l'application. */
        public static string emplacementDeBaseDeDonnées = string.Empty;

        /*Cette variable est le service mobile client. Elle est utile pour 
        accéder tout les choses sur le nuage (Azure) par rapport à Trado. */
        public static MobileServiceClient serviceMobile = new MobileServiceClient("https://tradoappli.azurewebsites.net");

        static AlgoliaClient client = new AlgoliaClient("C8DHJR7RVN", "131a851fb044bb0c3147f391a398f640");
        public static Index index = client.InitIndex("TradoObjet");

        /* Ceci est une fonction nommée Trado (Il doit toujours avoir une fonction 
        avec le nom de la classe), public/publique, ce qui veut dire qu'elle
        peut être elle aussi accéder par d'autres pages du projet. Entre 
        les parenthèses nous avons les paramètres nécessaires à cette fonction. 
        Cette fonction va prendre l'emplacement de la base de données spécifique 
        à chaque système opératif (iOS, Android, Windows). Entre les crochets, 
        nous avons ce que la fonction fait, les commandes qui seront exécutés. */
        public Trado(string emplacementDeBaseDeDonnéesSpécifique)
        {
            //Cette fonction initialize les bouttons, le texte, etc.
            InitializeComponent();

            FlowListView.Init();

            /*La page principale (MainPage) qui sera amener à l'écran par 
            la page de navigation (NavigationPage) est la page principale */
            MainPage = new NavigationPage(new PagePrincipale());

            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromRgb(0, 185, 0));

            MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.White);


            /* L'emplacement de base de données peut être utilisée par les autres page
            dans le projet général dépendant de la base de données spécifique. */
            emplacementDeBaseDeDonnées = emplacementDeBaseDeDonnéesSpécifique;
        }

        /* La fonction avec override veut dire que si quelque chose est commander dans cette
        fonction, cette fonction prendra les commandes et les effectuera en supprimant une 
        commande connexe si il y en a. La fonction avec void est une fonction qui performe 
        des actions et retourne rien. */
        protected override void OnStart()
        {
            // Donner des commandes pour quand l'application commence
        }

        protected override void OnSleep()
        {
            // Donner des commandes pour quand l'application dort
        }

        protected override void OnResume()
        {
            // Donner des commandes pour quand l'application reprent
        }
    }
}
