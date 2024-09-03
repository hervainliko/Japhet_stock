using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_Users
    {
        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_Users(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_Users args)
        {
            M_Users obj = new M_Users();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_Users args)
        {
            M_Users obj = new M_Users();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_Users args)
        {
            M_Users obj = new M_Users();
            obj.delete(args.fields);
            message = obj.callback;
        }
    }
}
