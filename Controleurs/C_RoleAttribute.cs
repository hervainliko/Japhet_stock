using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_RoleAttribute
    {
        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_RoleAttribute(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_RoleAttribute args)
        {
            M_role_attribute obj = new M_role_attribute();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_RoleAttribute args)
        {
            M_role_attribute obj = new M_role_attribute();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_RoleAttribute args)
        {
            M_role_attribute obj = new M_role_attribute();
            obj.delete(args.fields);
            message = obj.callback;
        }
    }
}
