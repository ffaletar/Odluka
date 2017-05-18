using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Odluka.Models;

namespace Odluka.ViewModels
{
    public class ProjektViewModel
    {

        public Projekt Projekt { get; set; }
        public int BrojAlternativa { get; set; }
        public List<Kriterij> ListaKriterija { get; set; }
        public List<Alternativa> ListaAlternativa { get; set; }

    }
}