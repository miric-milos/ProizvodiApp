using ASPProizvodiApp.Models.Infrastructure;
using ASPProizvodiApp.Models.ViewModels;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPProizvodiApp.Controllers
{
    [RoutePrefix(prefix: "api")]
    public class JsonController : Controller
    {
        [Route("proizvodi")]
        public ActionResult Index()
        {
            var proizvodi = JsonProizvodiManager.SviProizvodi();
            return View(proizvodi);
        }

        [Route("dodaj")]
        public ActionResult Dodaj()
        {
            return View();
        }

        [HttpPost]
        [Route("dodaj")]
        [ValidateAntiForgeryToken]
        public ActionResult Dodaj(ProizvodViewModel model)
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

                if (JsonProizvodiManager.DodajProizvod(proizvod))
                {
                    return Redirect("/api/proizvodi");
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
            var proizvod = JsonProizvodiManager.GetProizvod(id);

            if(proizvod != null)
            {
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
            // Ne postoji proizvod
            return View("Greska");
        }

        [HttpPost]
        [Route("proizvodi/izmeni")]
        [ValidateAntiForgeryToken]
        public ActionResult Izmeni(ProizvodViewModel model)
        {
            if (ModelState.IsValid)
            {
                var proizvod = JsonProizvodiManager.GetProizvod((int)TempData["id"]);
                if (proizvod != null)
                {
                    proizvod.Naziv = model.Naziv;
                    proizvod.Opis = model.Opis;
                    proizvod.Kategorija = model.Kategorija;
                    proizvod.Proizvodjac = model.Proizvodjac;
                    proizvod.Dobavljac = model.Dobavljac;
                    proizvod.Cena = model.Cena;

                    if (JsonProizvodiManager.IzmeniProizvod(proizvod))
                    {
                        return Redirect("/api/proizvodi");
                    }
                    
                    ModelState.AddModelError("", "Doslo je do greske pri izmeni");
                    return View(model);
                }
                // Proizvod ne postoji
                return View("Greska");
            }
            // Model nije u redu
            return View(model);
        }
    }
}