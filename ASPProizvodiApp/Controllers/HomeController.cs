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
            return View();
        }
    }
}