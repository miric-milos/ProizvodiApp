using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ASPProizvodiApp.Models.Infrastructure
{
    public static class ProizvodManager
    {
        public static IEnumerable<Proizvod> SviProizvodi()
        {
            var db = new ProizvodiContext();
            var proizvodi = db.Proizvodi.ToList();

            // Oslobodi resurse
            db.Dispose();

            return proizvodi;
        }

        public async static Task<bool> DodajProizvodAsync(Proizvod proizvod)
        {
            using (var db = new ProizvodiContext())
            {
                try
                {
                    db.Proizvodi.Add(proizvod);
                    await db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    // Ako se desi greska odkomentarisi
                    // throw;
                    return false;
                }
            }
            return true;
        }
    }
}