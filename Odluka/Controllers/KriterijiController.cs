using Odluka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Odluka.ViewModels;
using System.Data.Entity;
using Odluka.Helpers;

namespace Odluka.Controllers
{
    public class KriterijiController : Controller
    {
        // GET: Kriteriji
        public ActionResult Index(int id)
        {
            List<Kriterij> listaKriterija = new List<Kriterij>();
            using(AHPEntities db = new AHPEntities())
            {
                listaKriterija = db.Kriterijs.Include(x => x.Projekt1).Where(x => x.Projekt1.id == id && x.obrisan != true).OrderBy(a => a.idRoditelja).ToList();
            }

            PostaviPutanjeZaEvaluaciju(id);

            string pocetnaStranica = EvaluationController.PocetnaStranica();

            ProjektListViewModel projektKriterijViewModel = new ProjektListViewModel()
            {
                IdProjekta = id,
                ListaKriterija = listaKriterija,
                pocetnaStranica = pocetnaStranica
            };


            return View(projektKriterijViewModel);
        }

        public ActionResult UsporedbaKriterija(int projekt, int? kriterij)
        {
            PostaviPutanjeZaEvaluaciju(projekt);
            string sljedecaStranica = SljedecaStranicaEvaluacije();
            string prethodnaStranica = PrethodnaStranicaEvaluacije();

            AHPEntities db = new AHPEntities();

            List<UsporedbaKriterija> listaUsporedaba = db.UsporedbaKriterijas.Where(x => x.Kriterij.Projekt1.id == projekt && x.Kriterij3.Projekt1.id == projekt
                                                        && x.Kriterij.idRoditelja == kriterij && x.Kriterij3.idRoditelja == kriterij 
                                                        && x.Kriterij.obrisan != true && x.Kriterij3.obrisan != true).ToList();

            ProjektListViewModel projektKriterijViewModel = new ProjektListViewModel()
            {
                IdProjekta = projekt,
                ListaUsporedbaKriterija = listaUsporedaba,
                sljedecaStranica = sljedecaStranica,
                prethodnaStranica = prethodnaStranica
            };



            return View(projektKriterijViewModel);
        }

        public ActionResult NoviKriterij(int idProjekta, int idRoditelja, String nazivKriterija, String opisKriterija)
        {
            AHPEntities db = new AHPEntities();

            var kriterij = db.Set<Kriterij>();
            kriterij.Add(new Kriterij { naziv = nazivKriterija, opis = opisKriterija, projekt = idProjekta, idRoditelja = idRoditelja, konzistentno = false, obrisan = false });

            if (db.SaveChanges() != 0)
            {
                AzurirajUsporedbeKriterija(idProjekta);
                PostaviPutanjeZaEvaluaciju(idProjekta);
                return Redirect(Url.Action("Index", "Kriteriji", new { id = idProjekta }));
            }

            return Redirect(Url.Action("Index", "Kriteriji", new { id = idProjekta }));
        }

        public ActionResult BrisiKriterij(int id)
        {
            AHPEntities db = new AHPEntities();

            Kriterij kriterijZaBrisanje = db.Kriterijs.SingleOrDefault(b => b.id == id);
            int idProjekta = kriterijZaBrisanje.Projekt1.id;

            List<Kriterij> listaKriterijaZaBrisanje = DohvatiRoditeljaIDjecu(id);
            listaKriterijaZaBrisanje.Add(kriterijZaBrisanje);

            foreach(Kriterij kriterij in listaKriterijaZaBrisanje)
            {
                kriterij.obrisan = true;
                db.Kriterijs.Attach(kriterij);
                var entry = db.Entry(kriterij);
                entry.Property(x => x.obrisan).IsModified = true;
                db.SaveChanges();
            }

            PostaviPutanjeZaEvaluaciju(id);

            return Redirect(Url.Action("Index", "Kriteriji", new { id = idProjekta }));
        }

        public List<Kriterij> DohvatiRoditeljaIDjecu(int id)
        {
            List<Kriterij> listaKriterija = new List<Kriterij>();

            using (AHPEntities db = new AHPEntities())
            {
                listaKriterija = db.Kriterijs.Where(x => x.idRoditelja == id).ToList();
            }
            return listaKriterija;
        }

        public void AzurirajUsporedbeKriterija(int idProjekta)
        {
            AHPEntities db = new AHPEntities();
            List<Kriterij> kriteriji = db.Kriterijs.Include(x => x.Projekt1).Where(x => x.Projekt1.id == idProjekta).ToList();

            for(int i=0; i<kriteriji.Count; i++)
            {
                for(int j=0;j<kriteriji.Count; j++)
                {
                    int kriterij1 = kriteriji[i].id;
                    int kriterij2 = kriteriji[j].id;

                    bool a = db.UsporedbaKriterijas.Any(x => x.kriterij1 == kriterij1 && x.kriterij2 == kriterij2);
                    bool b = db.UsporedbaKriterijas.Any(x => x.kriterij1 == kriterij2 && x.kriterij2 == kriterij1);
                    
                    if (kriterij1 != kriterij2 && kriteriji[i].idRoditelja == kriteriji[j].idRoditelja)
                    {
                        if (!db.UsporedbaKriterijas.Any(x => x.kriterij1 == kriterij1 && x.kriterij2 == kriterij2 && x.Kriterij.obrisan != false && x.Kriterij3.obrisan != false) && !db.UsporedbaKriterijas.Any(x => x.kriterij1 == kriterij2 && x.kriterij2 == kriterij1 && x.Kriterij.obrisan != false && x.Kriterij3.obrisan != false))
                        {
                            var usporedbaKriterija = db.Set<UsporedbaKriterija>();
                            usporedbaKriterija.Add(new UsporedbaKriterija { kriterij1 = kriteriji[i].id, kriterij2 = kriteriji[j].id, vrijednost = null });
                            db.SaveChanges();
                        }
                    }
                    
                }
            }
        }

        public JsonResult DohvatiSveKriterije()
        {
            AHPEntities db = new AHPEntities();
            List<Kriterij> listaKriterija = db.Kriterijs.ToList();

            return Json(listaKriterija, JsonRequestBehavior.AllowGet);
        }

        public bool? UpisiUsporedbu(int kriterij1, int kriterij2, float vrijednost)
        {
            AHPEntities db = new AHPEntities();
            bool? konzistentno = false;
            //UsporedbaKriterija staraUsporedbaKriterija = db.UsporedbaKriterijas.SingleOrDefault(b => b.kriterij1 == kriterij1 && b.kriterij2 == kriterij2);
            UsporedbaKriterija staraUsporedbaKriterija = db.UsporedbaKriterijas.Where(b => (b.kriterij1 == kriterij1 && b.kriterij2 == kriterij2) || (b.kriterij1 == kriterij2 && b.kriterij2 == kriterij1)).SingleOrDefault();
            if (staraUsporedbaKriterija != null)
            {
                staraUsporedbaKriterija.vrijednost = vrijednost;
                db.SaveChanges();
                
                konzistentno = AzurirajKonzistentnost(staraUsporedbaKriterija.Kriterij.idRoditelja, staraUsporedbaKriterija.Kriterij.Projekt1.id);
                
                return konzistentno;
            }else
            {
                return false;
            }
        }

        public bool? AzurirajKonzistentnost(int? roditelj, int? projekt)
        {
            AHPEntities db = new AHPEntities();

            bool? konzistentno = false;

            if(roditelj != null)
            {
                Kriterij roditeljId = db.Kriterijs.SingleOrDefault(b => b.id == roditelj);

                roditeljId.konzistentno = ProvjeriKonzistentnost(roditelj);

                konzistentno = roditeljId.konzistentno;
            }
            else
            {
                Projekt projektId = db.Projekts.SingleOrDefault(b => b.id == projekt);

                projektId.konzistentno =  ProvjeriKonzistentnost(null);

                konzistentno = projektId.konzistentno;
            }

            db.SaveChanges();

            return konzistentno;
        }


        public bool ProvjeriKonzistentnost(int? roditelj)
        {
            AHPEntities db = new AHPEntities();
            List<UsporedbaKriterija> listaUsporedaba = new List<UsporedbaKriterija>();
            List<Kriterij> kriteriji = new List<Kriterij>();

            if (roditelj != null)
            {
                listaUsporedaba = db.UsporedbaKriterijas.Where(x => x.Kriterij.idRoditelja == roditelj || x.Kriterij3.idRoditelja == roditelj).ToList();
                kriteriji = db.Kriterijs.Where(x => x.idRoditelja == roditelj).ToList();
            }
            else
            {
                listaUsporedaba = db.UsporedbaKriterijas.Where(x => x.Kriterij.idRoditelja == null || x.Kriterij3.idRoditelja == roditelj).ToList();
                kriteriji = db.Kriterijs.Where(x => x.idRoditelja == null).ToList();
            }

            bool konzistentno = Izvrsitelj.KreirajNovuMatricuKriterija(kriteriji,listaUsporedaba);

            return konzistentno;
        }


        public void PostaviPutanjeZaEvaluaciju(int projektId)
        {
            AHPEntities db = new AHPEntities();
            List<int?> listaRoditelja = db.Kriterijs.Where(x => x.projekt == projektId && x.Kriterij2.obrisan != true).Select(x => x.idRoditelja).Distinct().ToList();
            List<Tuple<int, int?>> listaKriterija = new List<Tuple<int, int?>>();

            foreach (int? objekt in listaRoditelja)
            {
                Tuple<int, int?> tuple = Tuple.Create(projektId, objekt);
                listaKriterija.Add(tuple);
            }

            
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            for (int i = 0; i< listaKriterija.Count; i++)
            {
                string putanja = "/Kriteriji/UsporedbaKriterija";

                int projekt = listaKriterija[i].Item1;
                int? roditelj = listaKriterija[i].Item2;

                if(roditelj != null)
                {
                    putanja = putanja + "?projekt=" + projekt + "&kriterij=" + roditelj;
                }
                else
                {
                    putanja = putanja + "?projekt=" + projekt;
                }
                
                dictionary.Add(i, putanja);
            }

            EvaluationController.listaLinkova = dictionary;
        }

        public string SljedecaStranicaEvaluacije()
        {
            string url = Request.Url.ToString();
            string skraceniUrl = url.Substring(url.IndexOf("/Kriteriji"));

            string sljedecaStranica = EvaluationController.SljedecaStranica(skraceniUrl);

            return sljedecaStranica;
        }
        public string PrethodnaStranicaEvaluacije()
        {
            string url = Request.Url.ToString();
            string skraceniUrl = url.Substring(url.IndexOf("/Kriteriji"));

            string prethodnaStranica = EvaluationController.PrethodnaStranica(skraceniUrl);

            return prethodnaStranica;
        }


    }
}