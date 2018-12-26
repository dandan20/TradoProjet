using System;
using System.Collections.Generic;
using System.Text;

namespace TradoProjet.Model
{
    public class TradoCollectionObjet
    {
        //Id de la collection d'objets
        public string Id { get; set; }

        //Id d'un objet ajouté à la collection d'objets
        public TradoObjet tradoObjetId { get; set; }
    }
}
