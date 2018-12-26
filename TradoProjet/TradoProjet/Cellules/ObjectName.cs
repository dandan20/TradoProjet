using System;
using TradoProjet.Model;
using Xamarin.Forms;
using Image = Xamarin.Forms.Image;

namespace TradoProjet.Cellules
{
    public class NonVisibles : ViewCell
    {
        public bool isVisible = false;
        public NonVisibles()
        {
            Button button = new Button
            {
                Image = (FileImageSource)ImageSource.FromFile("ic_keyboard_arrow_right_white_24dp.png")
            };
            Image image = new Image();
            Label details = new Label();
            Label category = new Label();
            Label etat = new Label();
            Grid grille = new Grid
            {
                Padding = new Thickness(5),
                RowDefinitions =
                {
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto},
                    new RowDefinition {Height = GridLength.Auto}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
                },
            };
            grille.Children.Add(image, 0, 0);
            Grid.SetColumnSpan(image, 2);
            grille.Children.Add(details, 0, 1);
            Grid.SetColumnSpan(details, 2);
            grille.Children.Add(category, 0, 2);
            grille.Children.Add(etat, 1, 2);
            View = grille;

            image.BindingContext = this;
            image.SetBinding(Image.SourceProperty, nameof(TradoObjet.FichierDeImage));
            details.SetBinding(Label.TextProperty, nameof(TradoObjet.Details));
            etat.SetBinding(Label.TextProperty, nameof(TradoObjet.Etat));
            category.SetBinding(Label.TextProperty, nameof(TradoObjet.Categorie));

            button.Clicked += delegate
            {
                if (isVisible == false)
                {
                    grille.IsVisible = false;
                    button.Image = (FileImageSource)ImageSource.FromFile("ic_keyboard_arrow_right_white_24dp.png");
                    isVisible = true;
                }
                else if (isVisible == true)
                {
                    grille.IsVisible = true;
                    button.Image = (FileImageSource)ImageSource.FromFile("ic_keyboard_arrow_down_white_24dp.png");
                    isVisible = false;
                }
            };
        }
    }
}
