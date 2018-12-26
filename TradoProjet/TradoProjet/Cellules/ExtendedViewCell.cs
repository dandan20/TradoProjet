using System;
using System.Collections.Generic;
using System.Text;
using TradoProjet.Model;
using Xamarin.Forms;

namespace TradoProjet.Cellules
{
    public class ExtendedViewCell : ViewCell
    {
        public static readonly BindableProperty SelectedBackgroundColorProperty =
            BindableProperty.Create("SelectedBackgroundColor", typeof(Color), typeof(ExtendedViewCell), Color.Default);
        
        public Color SelectedBackgroundColor
        {
            get { return (Color)GetValue(SelectedBackgroundColorProperty); }
            set { SetValue(SelectedBackgroundColorProperty, value); }
        }

        public ExtendedViewCell()
        {
            Label etat = new Label();
            Label categorie = new Label();
            Label nom = new Label();
            Label details = new Label();
            Image image = new Image();
            Grid grille = new Grid
            {
                Padding = new Thickness(11),
                RowDefinitions = {
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition {Height = new GridLength(1, GridUnitType.Star)}
                }, ColumnDefinitions = {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(2, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(2, GridUnitType.Star)}
                }
            };
            grille.Children.Add(image, 0, 0);
            Grid.SetRowSpan(image, 2);
            grille.Children.Add(nom, 1, 0);
            Grid.SetColumnSpan(nom, 2);
            grille.Children.Add(details, 1, 1);
            Grid.SetColumnSpan(details, 2);
            grille.Children.Add(categorie, 1, 2);
            grille.Children.Add(etat, 2, 2);
            View = grille;

            image.BindingContext = this;
            image.SetBinding(Image.SourceProperty, nameof(TradoObjet.FichierDeImage));
            nom.SetBinding(Label.TextProperty, nameof(TradoObjet.Nom));
            details.SetBinding(Label.TextProperty, nameof(TradoObjet.Details));
            etat.SetBinding(Label.TextProperty, nameof(TradoObjet.Etat));
            categorie.SetBinding(Label.TextProperty, nameof(TradoObjet.Categorie));
        }
    }
}
