using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Odluka.Models;
using Odluka.ViewModels;

namespace Odluka.Controllers
{
    public class ProjektController : Controller
    {
        public ActionResult Info(int id)
        {
            Odluka.Models.
            AHPEntities db = new AHPEntities();
            Projekt projekt = db.Projekts.Where(c => c.id == id).Single();
            //db.Alternativas.

            List<Alternativa> listaAlternativa = (from alternativa in db.Alternativas
                                                              where alternativa.Projekt1.id == id
                                                              select alternativa).ToList();

            List<Kriterij> listaKriterija = (from kriterij in db.Kriterijs
                                                  where kriterij.Projekt1.id == id
                                                  select kriterij).ToList();

            ProjektViewModel projektViewModel = new ProjektViewModel()
            {
                Projekt = projekt,
                ListaAlternativa = listaAlternativa,
                ListaKriterija = listaKriterija,
                BrojAlternativa = listaAlternativa.Count()
            };


            return View(projektViewModel);
        }
        
        public ActionResult OsnovneInformacije(int id)
        {
            AHPEntities db = new AHPEntities();
            Projekt projekt = db.Projekts.Where(c => c.id == id).Single();
            //db.Alternativas.

            List<Alternativa> listaAlternativa = (from alternativa in db.Alternativas
                                                  where alternativa.Projekt1.id == id
                                                  select alternativa).ToList();

            List<Kriterij> listaKriterija = (from kriterij in db.Kriterijs
                                             where kriterij.Projekt1.id == id
                                             select kriterij).ToList();

            ProjektViewModel projektViewModel = new ProjektViewModel()
            {
                Projekt = projekt,
                ListaAlternativa = listaAlternativa,
                ListaKriterija = listaKriterija,
                BrojAlternativa = listaAlternativa.Count()
            };

            return View(projektViewModel);
        }

        public ActionResult NoviProjekt(String nazivProjekta, String opisProjekta)
        {



            return Redirect(Url.Action("Index", "Alternative", new { id = 17 }));
        }

        
    }
}