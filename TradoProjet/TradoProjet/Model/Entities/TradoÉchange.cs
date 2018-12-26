using System;
using System.Collections.Generic;
using System.Text;

namespace TradoProjet.Model
{
    public class TradoÉchange
    {//Id?
        public string ID { get; set; }

        public TradoCollectionObjet tradoObjetColl1 { get; set; }

        public TradoCollectionObjet tradoObjetColl2 { get; set; }

        public TradoUsager Usager1 { get; set; }

        public TradoUsager Usager2 { get; set; }
    }
}
