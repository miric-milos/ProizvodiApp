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
                using (var writer = File.CreateText(putanja))
                {
                    proizvod.Id =
                        proizvodi.Count() == 0 ? 0 : proizvodi[proizvodi.Count() - 1].Id + 1;

                    proizvodi.Add(proizvod);
                    new JsonSerializer().Serialize(writer, proizvodi);

                    return true;
                }
            }
            catch (Exception)
            {
                // throw;                
                return false;
            }
        }
    }
}