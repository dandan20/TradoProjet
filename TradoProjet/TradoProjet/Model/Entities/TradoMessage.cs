using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Entities
{
    public class TradoMessage
    {
        public string id { get; set; }

        public string groupId { get; set; }

        public string content { get; set; }

        public DateTime sendTime { get; set; }
    }
}
