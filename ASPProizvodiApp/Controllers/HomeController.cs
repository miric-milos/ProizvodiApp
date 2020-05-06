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
    [RoutePrefix(prefix: "")]
    public class HomeController : Controller
    {
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
        [ValidateAntiForgeryToken]
        [Route(template: "dodaj")]
        public async Task<ActionResult> Dodaj(ProizvodViewModel model)
        {
            if (ModelState.IsValid)
            {
                var proizvod = new Proizvod
                {
                    Naziv = model.Naziv,
                    Kategorija = model.Kategorija,
                    Opis = model.Opis,
                    Proizvodjac = model.Proizvodjac,
                    Dobavljac = model.Dobavljac,
                    Cena = model.Cena
                };

                if (await ProizvodManager.DodajProizvodAsync(proizvod))
                {
                    return Redirect(url: "/");
                }
            }
            // Model nije u redu
            ModelState.AddModelError("", "Doslo je do greske pri dodavanju proizvoda, pokusajte ponovo");
            return View(model);
        }
    }
}