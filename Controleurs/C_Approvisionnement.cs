using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_Approvisionnement
    {

        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_Approvisionnement(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_Approvisionnement args)
        {
            M_Approvcaisse obj = new M_Approvcaisse();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_Approvisionnement args)
        {
            M_Approvcaisse obj = new M_Approvcaisse();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_Approvisionnement args)
        {
            M_Approvcaisse obj = new M_Approvcaisse();
            obj.delete(args.fields);
            message = obj.callback;
        }
    }
}
