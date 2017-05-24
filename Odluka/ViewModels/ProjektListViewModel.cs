using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Odluka.Models;

namespace Odluka.ViewModels
{
    public class ProjektListViewModel
    {
        public int IdProjekta { get; set; }
        public List<Alternativa> ListaAlternativa { get; set; }
        public List<Projekt> ListaProjekata { get; set; }
        public List<Kriterij> ListaKriterija { get; set; }
        public List<UsporedbaKriterija> ListaUsporedbaKriterija { get; set; }
        public int idKorisnika { get; set; }

        public string sljedecaStranica { get; set; }
        public string prethodnaStranica { get; set; }
        public string pocetnaStranica { get; set; }

    }
}