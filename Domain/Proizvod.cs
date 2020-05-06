﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Proizvod
    {        
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public string  Kategorija { get; set; }
        public string Proizvodjac { get; set; }
        public string Dobavljac { get; set; }
        public decimal Cena { get; set; }
    }
}
