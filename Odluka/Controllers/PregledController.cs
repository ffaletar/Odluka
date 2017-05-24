using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Odluka.Controllers
{
    public class PregledController : Controller
    {
        // GET: Pregled
        public ActionResult Index()
        {
            return View();
        }
    }
}