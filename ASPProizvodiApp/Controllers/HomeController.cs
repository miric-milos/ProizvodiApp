using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASPProizvodiApp.Controllers
{
    [RoutePrefix(prefix: "")]
    public class HomeController : Controller
    {
        // [Route(template: "")]
        public ActionResult Index()
        {
            return View();
        }

        [Route(template: "dodaj")]
        public ViewResult Dodaj()
        {
            return View();
        }
    }
}