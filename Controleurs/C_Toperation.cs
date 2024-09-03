using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_Toperation
    {
        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_Toperation(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_Toperation args)
        {
            M_Toperation obj = new M_Toperation();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_Toperation args)
        {
            M_Toperation obj = new M_Toperation();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_Toperation args)
        {
            M_Toperation obj = new M_Toperation();
            obj.delete(args.fields);
            message = obj.callback;
        }
    }
}
