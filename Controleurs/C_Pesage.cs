using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_Pesage
    {
        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_Pesage(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_Pesage args)
        {
            M_Pesage obj = new M_Pesage();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_Pesage args)
        {
            M_Pesage obj = new M_Pesage();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_Pesage args)
        {
            M_Pesage obj = new M_Pesage();
            obj.delete(args.fields);
            message = obj.callback;
        }

        public void updateMention(C_Pesage args)
        {
            M_Pesage obj = new M_Pesage();
            obj.updateMention(args.fields);
            message = obj.callback;
        }
    }
}
