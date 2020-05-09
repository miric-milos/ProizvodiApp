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
    }
}