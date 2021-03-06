﻿using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

        public static Proizvod GetProizvod(int id)
        {
            using (var db = new ProizvodiContext())
            {
                var lista = db.Proizvodi.Where(p => p.Id == id);
                return lista.Count() != 1 ? null : lista.FirstOrDefault();
            }
        }

        public async static Task<bool> IzmeniProizvodAsync(Proizvod proizvod)
        {
            try
            {
                using (var db = new ProizvodiContext())
                {
                    // var stariProizvod = db.Proizvodi.SingleOrDefault(p => p.Id == proizvod.Id);
                    db.Proizvodi.AddOrUpdate(proizvod);
                    await db.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                // Odkomentarisi throw ako dodje do greske
                // throw;
                return false;
            }       
        }
    }
}