using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradoProjet.Model
{
    public class TradoObject
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string name { get; set; }

        public string category { get; set; }

        public string state { get; set; }

        public string imageFilename { get; set; }

        public string details { get; set; }
    }
}
