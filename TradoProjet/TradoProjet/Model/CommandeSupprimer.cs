using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace TradoProjet.Model
{
    public class ObjetsModele
    {
        public ObservableCollection<TradoObjet> TradoObjets { get; set; }

        public Command CommandeSupprimer
        {
            get
            {
                return new Command<TradoObjet>((tradoObjet) =>
                {
                    TradoObjets.Remove(tradoObjet);
                });
            }
        }

        public ObjetsModele()
        {
            
        }
    }
}
