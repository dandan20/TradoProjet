using System;
using System.Collections.Generic;
using System.Text;

namespace TradoProjet.Model
{
    public class TradoMessage
    {
        public string id { get; set; }

        public string texte { get; set; }

        public TradoUsager messageur { get; set; }
    }
}
