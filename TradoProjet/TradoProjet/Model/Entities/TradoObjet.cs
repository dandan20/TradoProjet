using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradoProjet.Model
{
    public class TradoObjet
    {
        public string Id { get; set; }

        //nom de l'objet, peut etre obtenu (get) et établi (set)
        public string Nom { get; set; }

        //nom de la catégorie de l'objet
        public string Categorie { get; set; }

        //état de l'objet
        public string Etat { get; set; }

        //Fichier de l'image de l'objet
        public string FichierDeImage { get; set; }

        //détails de l'objet
        public string Details { get; set; }

        //courriel de l'usager avec l'objet
        public string CourrielUsager { get; set; }
    }
}
