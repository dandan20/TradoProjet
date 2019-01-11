using System;
using System.Collections.Generic;
using System.Text;

namespace TradoProjet.Model
{
    public class TradoUsager
    {
        //Id de l'usager
        public string id { get; set; }

        //nom de l'usager
        public string Nom { get; set; }

        //nom d'usager de l'usager
        public string NomDUsager { get; set; }

        //courriel de l'usager
        public string Courriel { get; set; }

        //mot de passe de l'usager
        public string MotDePasse { get; set; }

        public int code { get; set; }

        public bool verifie = false;

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }
    }
}
