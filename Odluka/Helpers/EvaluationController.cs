using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Odluka.Helpers
{
    public class EvaluationController
    {
        public static Dictionary<int,string> listaLinkova = new Dictionary<int,string>();
        public static Dictionary<int,string> sljedecaStranica;
        public static Dictionary<int,string> prethodnaStranica;
        
        public static string SljedecaStranica(string trenutnaStranica)
        {
            int trenutnaKey = listaLinkova.FirstOrDefault(x => x.Value == trenutnaStranica).Key;
            string sljedecaStranica = listaLinkova.FirstOrDefault(x => x.Key == trenutnaKey + 1).Value;

            if (sljedecaStranica != null)
            {
                return sljedecaStranica;
            }
            else
            {
                return trenutnaStranica;
            }
        }

        public static string PrethodnaStranica(string trenutnaStranica)
        {
            int trenutnaKey = listaLinkova.FirstOrDefault(x => x.Value == trenutnaStranica).Key;
            string prethodnaStranica = listaLinkova.FirstOrDefault(x => x.Key == trenutnaKey - 1).Value;

            if(prethodnaStranica != null)
            {
                return prethodnaStranica;
            }else
            {
                return trenutnaStranica;
            }
        }

        public static string PocetnaStranica()
        {
            string pocetnaStranica = listaLinkova.FirstOrDefault(x => x.Key == 0).Value;
            
            return pocetnaStranica;
        }


    }
}