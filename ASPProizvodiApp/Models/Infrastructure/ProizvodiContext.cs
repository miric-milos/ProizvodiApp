using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPProizvodiApp.Models.Infrastructure
{
    public class ProizvodiContext : DbContext
    {
        public ProizvodiContext() : base("WMKodTestBaza") { }

        public DbSet<Domain.Proizvod> Proizvodi { get; set; }
    }
}