﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WIN.BASEREUSE;

namespace UilDBIscritti.Domain
{
    public class Territorio :AbstractPersistenceObject
    {
        public Territorio() { }
        public Territorio(int id) { this.Key = new Key(id); }
        public string Alias { get; set; }
        public string Province { get; set; }



    }
}
