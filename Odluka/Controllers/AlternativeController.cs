using Odluka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Odluka.ViewModels;

namespace Odluka.Controllers
{
    public class AlternativeController : Controller
    {
        // GET: Alternative
        public ActionResult Index(int id)
        {
            AHPEntities db = new AHPEntities();

            List<Alternativa> listaAlternativa = (from alternativa in db.Alternativas
                                                  where alternativa.Projekt1.id == id
                                                  select alternativa).ToList();

            ProjektListViewModel projektAlternativaViewModel = new ProjektListViewModel() {
                IdProjekta = id,
                ListaAlternativa = listaAlternativa
            };

            return View(projektAlternativaViewModel);
        }

        //[HttpPost]
        public ActionResult NovaAlternativa(int idProjekta, String nazivAlternative, String opisAlternative)
        {
            AHPEntities db = new AHPEntities();

            var alternativa = db.Set<Alternativa>();
            alternativa.Add(new Alternativa { naziv = nazivAlternative, opis = opisAlternative, projekt = idProjekta });

            if(db.SaveChanges() != 0){
                return Redirect(Url.Action("OsnovneInformacije", "Projekt", new { id = idProjekta }));
            }

            return Redirect(Url.Action("OsnovneInformacije", "Projekt", new { id = idProjekta }));

        }
        public ActionResult UrediAlternativu(int idAlternative, String nazivAlternative, String opisAlternative)
        {
            AHPEntities db = new AHPEntities();

            Alternativa staraAlternativa = db.Alternativas.SingleOrDefault(b => b.id == idAlternative);
            if (staraAlternativa != null)
            {
                staraAlternativa.naziv = nazivAlternative;
                staraAlternativa.opis = opisAlternative;
                db.SaveChanges();
                return Redirect(Url.Action("Index", "Alternative", new { id = staraAlternativa.Projekt1.id }));
            }
            else
            {
                return Redirect(Url.Action("Index", "Alternative", new { id = staraAlternativa.Projekt1.id }));
            }



        }
        public ActionResult BrisiAlternativu(int idAlternative)
        {
            AHPEntities db = new AHPEntities();

            Alternativa alternativaZaBrisanje = db.Alternativas.SingleOrDefault(b => b.id == idAlternative);
            int idProjekta = alternativaZaBrisanje.Projekt1.id;
            if (alternativaZaBrisanje != null)
            {
                db.Alternativas.Attach(alternativaZaBrisanje);
                db.Alternativas.Remove(alternativaZaBrisanje);
                db.SaveChanges();
                return Redirect(Url.Action("Index", "Alternative", new { id = idProjekta }));
            }
            else
            {
                return Redirect(Url.Action("Index", "Alternative", new { id = idProjekta }));
            }


        }
    }
}