using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Odluka.Models;
using Odluka.ViewModels;

namespace Odluka.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(int? id)
        {
            int idKorisnika;

            idKorisnika = id ?? default(int);


            List<Projekt> listaProjekata = null;
            if (id == null)
            {

            }else
            {
                AHPEntities db = new AHPEntities();
                listaProjekata = db.Projekts.Where(c => c.korisnik == id).OrderByDescending(c => c.zadnjaPromjena).ToList();
            }


            ProjektListViewModel projektAlternativaViewModel = new ProjektListViewModel()
            {
                idKorisnika = idKorisnika,
                ListaProjekata = listaProjekata
            };


            return View(projektAlternativaViewModel);
        }
    }
}