using ASPProizvodiApp.Models.Infrastructure;
using ASPProizvodiApp.Models.ViewModels;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASPProizvodiApp.Controllers
{
    [RoutePrefix(prefix: "db")]
    public class BazaController : Controller
    {
        // Kontroler je ovako nazvan radi primera
        [Route(template: "proizvodi")]
        public ActionResult Index()
        {
            var model = ProizvodManager.SviProizvodi();
            return View(model);
        }

        [Route(template: "dodaj")]
        public ViewResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        [Route(template: "dodaj")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Dodaj(ProizvodViewModel model)
        {
            if (ModelState.IsValid)
            {
                var proizvod = new Proizvod
                {
                    Naziv = model.Naziv,
                    Opis = model.Opis,
                    Kategorija = model.Kategorija,
                    Proizvodjac = model.Proizvodjac,
                    Dobavljac = model.Dobavljac,
                    Cena = model.Cena
                };

                if(await ProizvodManager.DodajProizvodAsync(proizvod))
                {
                    return Redirect("/db/proizvodi");
                }
                // Nije uspelo dodavanje
                ModelState.AddModelError("", "Doslo je do greske pri dodavanju.");
                return View(model);
            }
            // Model nije u redu
            return View(model);
        }
    }
}