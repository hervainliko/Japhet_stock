using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_Descpese
    {
        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_Descpese(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_Descpese args)
        {
            M_Descpese obj = new M_Descpese();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_Descpese args)
        {
            M_Descpese obj = new M_Descpese();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_Descpese args)
        {
            M_Descpese obj = new M_Descpese();
            obj.delete(args.fields);
            message = obj.callback;
        }




    }
}
