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
        public List<Kriterij> ListaKriterija { get; set; }
        public List<UsporedbaKriterija> ListaUsporedbaKriterija { get; set; }
    }
}