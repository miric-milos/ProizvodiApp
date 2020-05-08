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

                if (await ProizvodManager.DodajProizvodAsync(proizvod))
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

        [Route("proizvodi/{id}")]
        public ActionResult Izmeni(int id = -1)
        {
            var proizvod = ProizvodManager.GetProizvod(id);

            if (proizvod != null)
            {
                // Koriscenjem TempData ne izlazemo id korisniku
                TempData["id"] = proizvod.Id;
                return View(new ProizvodViewModel 
                {
                    Naziv = proizvod.Naziv,
                    Opis = proizvod.Opis,
                    Kategorija = proizvod.Kategorija,
                    Proizvodjac = proizvod.Proizvodjac,
                    Dobavljac = proizvod.Dobavljac,
                    Cena = proizvod.Cena
                });
            }
            // Ne otkrivati da proizvod ne postoji
            return View("Greska");
        }

        [HttpPost]
        [Route("proizvodi/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Izmeni(ProizvodViewModel model)
        {
            if (ModelState.IsValid)
            {
                var proizvod = ProizvodManager.GetProizvod((int)TempData["id"]);
                if (proizvod != null)
                {
                    proizvod.Naziv = model.Naziv;
                    proizvod.Opis = model.Opis;
                    proizvod.Kategorija = model.Kategorija;
                    proizvod.Proizvodjac = model.Proizvodjac;
                    proizvod.Dobavljac = model.Dobavljac;
                    proizvod.Cena = model.Cena;

                    if(await ProizvodManager.IzmeniProizvodAsync(proizvod))
                    {
                        return Redirect("/db/proizvodi");
                    }
                    // Greska pri izmeni
                    ModelState.AddModelError("", "Doslo je do greske pri izmeni");
                    return View(model);
                }
                // Proizvod nije pronadjen
                return View("Greska");
            }
            // Model nije u redu
            return View(model);
        }
    }
}