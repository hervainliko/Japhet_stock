using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_Tepargne
    {

        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_Tepargne(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_Tepargne args)
        {
            M_Tepargne obj = new M_Tepargne();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_Tepargne args)
        {
            M_Tepargne obj = new M_Tepargne();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_Tepargne args)
        {
            M_Tepargne obj = new M_Tepargne();
            obj.delete(args.fields);
            message = obj.callback;
        }
    }
}
