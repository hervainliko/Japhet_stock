using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Apps
{
    internal class Descpesages
    {
        public static List<Descpesages> listDetail = new List<Descpesages>();

        public int id { get; set; }
        public int NumPese { get; set; }
        public string poids { get; set; }
        public string PU { get; set; }
        public string pourcentage { get; set; }
        public string indice { get; set; }
        public string numerateur { get; set; }
        public string total { get; set; }
        public string reference { get; set; }
        public string bourse { get; set; }
        public string teneur { get; set; }
    }
}
