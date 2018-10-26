using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradoProjet.Model
{
    public class TradoObjet
    {
        //une clé principale, qui augmente automatiquement, c'est l'id de l'objet (nombre entier (int)).
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        //nom de l'objet, peut etre obtenu (get) et établi (set)
        public string nom { get; set; }

        //nom de la catégorie de l'objet
        public string catégorie { get; set; }

        //état de l'objet
        public string état { get; set; }

        //Fichier de l'image de l'objet
        public string fichierDeImage { get; set; }

        //détails de l'objet
        public string détails { get; set; }
    }
}
