using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPProizvodiApp.Models.Infrastructure
{
    public class ProizvodiContext : DbContext
    {
        public DbSet<Domain.Proizvod> MyProperty { get; set; }
    }
}