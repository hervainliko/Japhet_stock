using Japhet.Modeles;
using Japhet.Modeles.report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_mouvement
    {
        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_mouvement(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_mouvement args)
        {
            M_Mouvement_caisse obj = new M_Mouvement_caisse();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_mouvement args)
        {
            M_Mouvement_caisse obj = new M_Mouvement_caisse();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_mouvement args)
        {
            M_Mouvement_caisse obj = new M_Mouvement_caisse();
            obj.delete(args.fields);
            message = obj.callback;
        }
    }
}
