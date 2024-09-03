using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Services
{
    internal class Item
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
