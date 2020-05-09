using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ASPProizvodiApp.Models.Infrastructure
{
    public static class JsonProizvodiManager
    {
        private static string putanja =
            HttpContext.Current.Server.MapPath("~/Content/proizvodi.json");

        private static void SacuvajIzmene(IEnumerable<Proizvod> proizvodi)
        {
            using (var writer = File.CreateText(putanja))
            {
                new JsonSerializer().Serialize(writer, proizvodi);
            }
        }

        public static IEnumerable<Proizvod> SviProizvodi()
        {
            if (File.Exists(putanja))
            {
                var sadrzaj = File.ReadAllText(putanja);
                var proizvodi = JsonConvert.DeserializeObject<IEnumerable<Proizvod>>(sadrzaj);
                if (proizvodi != null)
                {
                    return proizvodi;
                }
            }
            // Ne postoji fajl ili je prazan
            return Enumerable.Empty<Proizvod>();
        }

        public static bool DodajProizvod(Proizvod proizvod)
        {
            try
            {
                var proizvodi = SviProizvodi().ToList();
                proizvod.Id =
                    proizvodi.Count == 0 ? 0 : proizvodi[proizvodi.Count - 1].Id + 1;

                proizvodi.Add(proizvod);
                SacuvajIzmene(proizvodi);

                return true;
            }
            catch (Exception)
            {
                // throw;                
                return false;
            }
        }

        public static Proizvod GetProizvod(int id)
        {
            var proizvodi = SviProizvodi().Where(p => p.Id == id);
            return proizvodi.Count() != 1 ? null : proizvodi.FirstOrDefault();
        }

        public static bool IzmeniProizvod(Proizvod proizvod)
        {
            var proizvodi = SviProizvodi().ToList();

            for (int i = 0; i < proizvodi.Count; ++i)
            {
                if (proizvodi[i].Id == proizvod.Id)
                {
                    proizvodi[i] = proizvod;
                    break;
                }
            }

            try
            {
                SacuvajIzmene(proizvodi);
                return true;
            }
            catch (Exception)
            {
                // throw;
                return false;
            }
        }
    }
}