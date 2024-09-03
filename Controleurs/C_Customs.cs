﻿using Japhet.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Japhet.Controleurs
{
    internal class C_Customs
    {
        Dictionary<string, string> fields { get; set; }
        public Dictionary<string, string> message;
        public C_Customs(Dictionary<string, string> fields)
        {
            this.fields = fields;
        }
        public void add(C_Customs args)
        {
            M_Customes obj = new M_Customes();
            obj.insert(args.fields);
            message = obj.callback;
        }
        public void update(C_Customs args)
        {
            M_Customes obj = new M_Customes();
            obj.update(args.fields);
            message = obj.callback;
        }
        public void delete(C_Customs args)
        {
            M_Customes obj = new M_Customes();
            obj.delete(args.fields);
            message = obj.callback;
        }
    }
}
