using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Odluka.Models;

namespace Odluka.Helpers
{
    public static class Izvrsitelj
    {
        public static bool KreirajNovuMatricuKriterija(List<Kriterij> primljenaListaKriterija, List<UsporedbaKriterija> usporedbe)
        {
            int brojKriterija = primljenaListaKriterija.Count();
            double?[,] matricaKriterija = new double?[brojKriterija, brojKriterija];

            bool matricaPotpuna = true;


            for (int i = 0; i < primljenaListaKriterija.Count; i++)
            {
                Kriterij kriterij1 = new Kriterij();
                kriterij1 = primljenaListaKriterija[i];
                for (int j = 0; j < primljenaListaKriterija.Count; j++)
                {
                    if(i == j)
                    {
                        matricaKriterija[i, j] = 1;
                    }else if(i < j)
                    {
                        Kriterij kriterij2 = new Kriterij();
                        kriterij2 = primljenaListaKriterija[j];
                        double? vrijednost;
                        UsporedbaKriterija usp = usporedbe.Find(x => (x.kriterij1 == kriterij1.id && x.kriterij2 == kriterij2.id));
                        if (usp != null)
                        {
                            vrijednost = usp.vrijednost;
                            matricaKriterija[i, j] = vrijednost;
                            matricaKriterija[j, i] = 1 / vrijednost;
                        }else
                        {
                            matricaPotpuna = false;
                            break;
                        }
                    }
                    
                }
            }
            bool konzistentno = false;
            if (matricaPotpuna)
            {
                konzistentno = ProvjeriKonzistentnost(matricaKriterija);
            }else
            {
                konzistentno = false;
            }
            

            return konzistentno;
        }


        public static bool ProvjeriKonzistentnost(double?[,] matrKriterija)
        {
            List<double?> zbrojStupaca = new List<double?>();
            List<double?> zbrojRedakaPrepravljenogPolja = new List<double?>();
            List<double?> konacniProsjek = new List<double?>();
            double? konacniProsjekZbroja = 0;
            List<double?> prosjekRetka = new List<double?>();
            List<double?> prosjekRedakaPrepravljenogPolja = new List<double?>();
            List<double?> vrijednostKriterija = new List<double?>();
            List<double?> konzistentnostRetci = new List<double?>();
            bool konzistentno = false;

            double? CI = 0;
            double? CR = 0;
            double? lambdaZbroj = 0;
            double? lambda = 0;
            int brojKriterija = (int)Math.Sqrt(matrKriterija.Length);

            double?[,] prepravljenoPolje = new double?[brojKriterija, brojKriterija];
            double?[,] zadnjePrepravljenoPolje = new double?[brojKriterija, brojKriterija];
            //ovaj dio koda se ponavlja, možda bi se moglo spojiti u jedno, zbog efikasnosti, odnosno zbog redundantnog izvršavanja for petlji

            //zbroj stupaca matrice
            for (int j = 0; j < Math.Sqrt(matrKriterija.Length); j++)
            {
                double? zbroj = 0;

                for (int i = 0; i < Math.Sqrt(matrKriterija.Length); i++)
                {
                    zbroj = zbroj + matrKriterija[i, j];
                }

                zbrojStupaca.Add(zbroj);
            }
            //zbroj redaka matrice
            for (int i = 0; i < Math.Sqrt(matrKriterija.Length); i++)
            {
                double? zbroj = 0;

                for (int j = 0; j < Math.Sqrt(matrKriterija.Length); j++)
                {
                    zbroj = zbroj + matrKriterija[i, j];
                }

                prosjekRetka.Add(zbroj / Math.Sqrt(matrKriterija.Length));
            }

            for (int i = 0; i < Math.Sqrt(matrKriterija.Length); i++)
            {
                //double zbrojRedaka = 0;
                string ime;
                for (int j = 0; j < Math.Sqrt(matrKriterija.Length); j++)
                {
                    prepravljenoPolje[i, j] = matrKriterija[i, j] / zbrojStupaca[j];
                    //zbrojRedaka = zbrojRedaka + prepravljenoPolje[i, j];
                }
            }

            for (int i = 0; i < Math.Sqrt(prepravljenoPolje.Length); i++)
            {
                double? zbroj = 0;

                for (int j = 0; j < Math.Sqrt(prepravljenoPolje.Length); j++)
                {
                    zbroj = zbroj + prepravljenoPolje[i, j];
                }

                prosjekRedakaPrepravljenogPolja.Add(zbroj / Math.Sqrt(prepravljenoPolje.Length));
            }

            for (int i = 0; i<Math.Sqrt(matrKriterija.Length); i++)
            {
                for (int j = 0; j < prosjekRetka.Count; j++)
                {
                    zadnjePrepravljenoPolje[i, j] = matrKriterija[i, j] * prosjekRedakaPrepravljenogPolja[j];
                }
            }

            for (int i = 0; i < Math.Sqrt(zadnjePrepravljenoPolje.Length); i++)
            {
                double? zbroj = 0;
                for (int j = 0; j < Math.Sqrt(zadnjePrepravljenoPolje.Length); j++)
                {
                    zbroj = zbroj + zadnjePrepravljenoPolje[i, j];
                }
                zbrojRedakaPrepravljenogPolja.Add(zbroj);
            }

            for (int i = 0; i < Math.Sqrt(zadnjePrepravljenoPolje.Length); i++)
            {
                double? zbroj = 0;

                konacniProsjek.Add(zbrojRedakaPrepravljenogPolja[i] / zadnjePrepravljenoPolje[i, i]);
            }

            konacniProsjekZbroja = konacniProsjek.Average();
            

            lambda = konzistentnostRetci.Sum();


            CI = (konacniProsjekZbroja - Math.Sqrt(matrKriterija.Length)) / ((Math.Sqrt(matrKriterija.Length) - 1));

            CR = CI / DohvatiRI((int)Math.Sqrt(matrKriterija.Length));

            if (CR > 0.1)
            {
                konzistentno = false;
            }
            else
            {
                konzistentno = true;
            }

            return konzistentno;
        }


        public static double DohvatiRI(int brojKriterija)
        {
            double RI = 0;
            switch (brojKriterija)
            {
                case 1:
                    RI = 0;
                    break;
                case 2:
                    RI = 0;
                    break;
                case 3:
                    RI = 0.58;
                    break;
                case 4:
                    RI = 0.9;
                    break;
                case 5:
                    RI = 1.12;
                    break;
                case 6:
                    RI = 1.24;
                    break;
                case 7:
                    RI = 1.32;
                    break;
                case 8:
                    RI = 1.41;
                    break;
                case 9:
                    RI = 1.45;
                    break;
                case 10:
                    RI = 1.49;
                    break;
                default:
                    RI = 0;
                    break;
            }

            return RI;
        }
    }
}