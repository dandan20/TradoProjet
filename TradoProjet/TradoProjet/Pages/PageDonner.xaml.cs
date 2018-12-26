﻿using SQLite;
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
        TradoObjet selectedObjet = null;

        public string Courriel;
        public PageDonner(string courriel)
        {
            InitializeComponent();
            Courriel = courriel;
        }

        //Cette fonction marche quand la page apparait
        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection conn = new SQLiteConnection(Trado.emplacementDeBaseDeDonnées))
            {
                //création d'une table d'objets de l'usager
                conn.CreateTable<TradoObjet>();
                //Cette table en forme de liste est mise dans une variable nommée TradoObjets.
                var tradoObjets = conn.Table<TradoObjet>().ToList();
                //La liste d'objets va dans la liste créé dans la partie XAML (ObjetsListView)
                ObjetsListView.ItemsSource = tradoObjets;
            }
        }

        //Cette fonction s'actionne quand le bouton ajouter un objet est clické
        private void AjouterObjetButton_Clicked(object sender, EventArgs e)
        {
            //Quand le bouton a été clické, il y a navigation a la page de création d'ajout d'objet
            Navigation.PushAsync(new PageAjouterObjet(Courriel));
        }

        private void ObjetsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedObjet = (TradoObjet)e.SelectedItem;
        }

        private async void SupprimerObjet(object sender, EventArgs e)
        {
            var reponse = await DisplayAlert("Etes vous certain?", "Voulez-vous supprimez cet objet?", "Oui", "Non");
            if(reponse == true)
            {
                //Supprimer
                var button = sender as MenuItem;
                var objet = button?.BindingContext as TradoObjet;
                var vm = BindingContext as ObjetsModele;
                vm?.CommandeSupprimer.Execute(objet);
                await Trado.serviceMobile.GetTable<TradoObjet>().DeleteAsync(selectedObjet);
            }
        }

        private void ModifierObjet(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PageModifierObjet(selectedObjet));
        }
    }
}