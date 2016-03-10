using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testapp.Models
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public double payment { get; set; }
        public DateTime creation_date { get; set; }
    }
}