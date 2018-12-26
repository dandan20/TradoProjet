using System;
using System.Collections.Generic;
using System.Text;

namespace TradoProjet.Model
{
    public class TradoConversation
    {
        public string Id { get; set; }

        public TradoCollectionUsager collectionUsager { get; set; }

        public TradoCollectionMessage collectionMessage { get; set; }
    }
}
