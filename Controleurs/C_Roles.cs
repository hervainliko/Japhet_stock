using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_Roles
    {

        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_Roles(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_Roles args)
        {
            M_Roles obj = new M_Roles();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_Roles args)
        {
            M_Roles obj = new M_Roles();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_Roles args)
        {
            M_Roles obj = new M_Roles();
            obj.delete(args.fields);
            message = obj.callback;
        }
    }
}
