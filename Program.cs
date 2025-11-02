using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Foci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var j1 = new Jatekos("Sallai Roland", "csatár", 27, 12000000);
            var j2 = new Jatekos("Szoboszlai Dominik", "középpályás", 23, 45000000);
            var j3 = new Jatekos("Fiola Attila", "védő", 34, 3250000);
            var j4 = new Jatekos("Gulácsi Péter", "kapus", 34, 8000000);
            var j5 = new Jatekos("Nego Loic", "védő", 31, 3000000);

            Jatekos.Jatekosok.Add(j1);
            Jatekos.Jatekosok.Add(j2);
            Jatekos.Jatekosok.Add(j3);
            Jatekos.Jatekosok.Add(j4);
            Jatekos.Jatekosok.Add(j5);

            //1.feladat:

            Console.WriteLine("Legidősebb játékos: {0}", Jatekos.Legidosebb());

            //2.feladat:

            Console.WriteLine("Átlag piaci érték: {0} EUR", Jatekos.Atlag());

            //3.feladat:

            if (Jatekos.Van())
            {
                Console.WriteLine("Van 20000000 EUR feletti játékos.");
            }
            else
            {
                Console.WriteLine("Nincsen 20000000 EUR feletti játékos.");
            }

            //4.feladat:

            Console.WriteLine("Átlag fölött értékelt játékosok:");
            foreach (var nev in Jatekos.AtlagFelett()) Console.WriteLine(nev);

            //5.feladat:

            Console.WriteLine("{0} játékos fiatalabb az átlag életkornál és alatta értékelik az átlag értéknek.",Jatekos.FiatalEsOlcso());

            Console.ReadKey();
        }
    }

    public class Jatekos
    {
        public static List<Jatekos> Jatekosok = new List<Jatekos>();

        private string nev;
        private string poszt;
        private int kor;
        private int piaciErtek;

        public string Nev
        {
            get => nev;
            set => nev = (value.Length > 1) ? value : throw new Exception("Név minimum 2 karakter!");
        }

        public string Poszt
        {
            get => poszt;
            set => poszt = (value.Length > 2) ? value : throw new Exception("Poszt minimum 3 karakter!");
        }

        public int Kor
        {
            get => kor;
            set => kor = (value >= 16 && value <= 45) ? value : throw new Exception("Életkor 16-45 között!");
        }

        public int PiaciErtek
        {
            get => piaciErtek;
            set => piaciErtek = (value >= 0) ? value : throw new Exception("Érték nem lehet negatív!");
        }

        public Jatekos(string nev, string poszt, int kor, int ertek)
        {
            Nev = nev;
            Poszt = poszt;
            Kor = kor;
            PiaciErtek = ertek;
        }

        public static string Legidosebb()
        {
            int max = Jatekosok[0].Kor;
            string nev = "";

            foreach (var j in Jatekosok)
                if (j.Kor > max) max = j.Kor;

            foreach (var j in Jatekosok)
                if (j.Kor == max) nev = j.Nev;

            return nev;
        }

        public static double Atlag()
        {
            double osszeg = 0;
            foreach (var j in Jatekosok) osszeg += j.PiaciErtek;
            return osszeg / Jatekosok.Count;
        }

        public static bool Van()
        {
            foreach (var j in Jatekosok)
            {
                if (j.PiaciErtek > 20000000)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<string> AtlagFelett()
        {
            double atlag = Atlag();
            List<string> lista = new List<string>();
            foreach (var j in Jatekosok)
                if (j.PiaciErtek > atlag) lista.Add(j.Nev);
            return lista;
        }

        public static int FiatalEsOlcso()
        {
            double osszegE = 0;
            double osszegK = 0;
            foreach (var j in Jatekosok)
            {
                osszegE += j.PiaciErtek;
            }
            foreach (var j in Jatekosok)
            {
                osszegK += j.Kor;
            }
            double atlagE = osszegE / Jatekosok.Count;
            double atlagK = osszegK / Jatekosok.Count;

            int db = 0;
            foreach (var j in Jatekosok)
                if (j.Kor < atlagK && j.PiaciErtek < atlagE)
                {
                    db++;
                }
            return db;
        }
    }
}
