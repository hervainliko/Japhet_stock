using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_Dateexercice
    {
        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_Dateexercice(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_Dateexercice args)
        {
            M_Dateexercice obj = new M_Dateexercice();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_Dateexercice args)
        {
            M_Dateexercice obj = new M_Dateexercice();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_Dateexercice args)
        {
            M_Dateexercice obj = new M_Dateexercice();
            obj.delete(args.fields);
            message = obj.callback;
        }
    }
}
