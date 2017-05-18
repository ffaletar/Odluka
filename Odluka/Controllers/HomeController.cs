using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Odluka.Models;

namespace Odluka.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int? id)
        {
            List<Projekt> listaProjekata = null;
            if (id == null)
            {

            }else
            {
                AHPEntities db = new AHPEntities();
                listaProjekata = db.Projekts.Where(c => c.korisnik == id).ToList();
            }
            

            return View(listaProjekata);
        }
    }
}