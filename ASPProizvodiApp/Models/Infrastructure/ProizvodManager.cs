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
        public static List<Proizvod> SviProizvodi()
        {
            var db = new ProizvodiContext();
            var proizvodi = db.Proizvodi.ToList();

            // Oslobodi memoriju
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
                catch (Exception ex)
                {
                    // Ako se desi greska odkomentarisi
                    // throw ex;
                    return false;
                }
            }
            return true;
        }
    }
}