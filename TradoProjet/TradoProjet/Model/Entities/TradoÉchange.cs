using System;
using System.Collections.Generic;
using System.Text;

namespace TradoProjet.Model
{
    public class TradoÉchange
    {//Id?
        public string id { get; set; }

        public TradoObjet[] tradoObjetsGet { get; set; }

        public TradoObjet[] tradoObjetsGive { get; set; }

        public TradoUsager UsagerInitial { get; set; }

        public TradoUsager Usager2 { get; set; }

        public bool acceptationInitial { get; set; }

        public bool acceptation2 { get; set; }

        public bool acceptation = false;

        public bool termine = false;

        public bool setup = false;

        public bool confirmInitial = false;

        public bool confirm2 = false;

        public bool confirmed = false;

        public TradoUsager confirmUser { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public TradoÉchange()
        {
            if(acceptationInitial == true && acceptation2 == true)
            {
                acceptation = true;
            } else
            {
                acceptation = false;
            }

            if(confirmInitial == true && confirm2 == true)
            {
                confirmed = true;
            } else
            {
                confirmed = false;
            }
        }
    }
}
