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

            List<Dnevnik> listaZapisaIzDnevnika = (from dnevnik in db.Dnevniks
                                             where dnevnik.Projekt1.id == id orderby dnevnik.vrijeme descending
                                             select dnevnik).ToList();

            ProjektViewModel projektViewModel = new ProjektViewModel()
            {
                Projekt = projekt,
                ListaAlternativa = listaAlternativa,
                ListaKriterija = listaKriterija,
                ListaZapisaDnevnika = listaZapisaIzDnevnika,
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

        public ActionResult NoviProjekt(String naziv, String opis, int korisnik)
        {
            AHPEntities db = new AHPEntities();
            DateTime date = DateTime.Now;

            Projekt projekt = new Projekt { naziv = naziv, opis = opis, korisnik = korisnik, datum = date, zadnjaPromjena = date, konzistentno = false, fazaProjekta = 3 };

            var projekti = db.Set<Projekt>();
            projekti.Add(projekt);

            if (db.SaveChanges() != 0)
            {
                int idProjekta = projekt.id;
                return Redirect(Url.Action("Info", "Projekt", new { id = idProjekta }));
            }

            return null;
        }

        
    }
}