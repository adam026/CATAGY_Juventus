using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CATAGY_Juventus
{
    class Jatekos
    {
        public int Mezszam { get; set; }
        public string Nev { get; set; }
        public string Nemzetiseg { get; set; }
        public string Poszt { get; set; }
        public int Szulev { get; set; }
        public int Eletkor => DateTime.Today.Year - Szulev;

        public Jatekos(string sor)
        {
            var buffer = sor.Split(';');
            Mezszam = int.Parse(buffer[0]);
            Nev = buffer[1];
            Nemzetiseg = buffer[2];
            Poszt = buffer[3];
            Szulev = int.Parse(buffer[4]);
        }
    }
    internal class Program
    {
        public static List<Jatekos> jatekosok = new List<Jatekos>();
        static void Main(string[] args)
        {
            Beolvasas();
            F01();
            F02();
            F03();
            F04();
            F05();
            F06();
            F07();
            F08();
            Bekeres();
            Kiiras();
        }

        private static void Kiiras()
        {
            var sw = new StreamWriter(@"..\..\RES\hatvedek.txt", false, Encoding.UTF8);
            foreach (var jatekos in jatekosok)
            {
                if (jatekos.Poszt == "hátvéd")
                {
                    var csaladnev = jatekos.Nev.Split(' ')[1];
                    sw.WriteLine($"{csaladnev} {jatekos.Eletkor}");
                }
            }
            sw.Close();
            Console.WriteLine("10. Feladat: Kiírás kész!");
        }

        private static void Bekeres()
        {
            Console.Write("9. Feladat: Kérek egy mezszámot: ");
            var mezszam = int.Parse(Console.ReadLine());
            var talalat = 0;
            foreach (var jatekos in jatekosok)
            {
                if (jatekos.Mezszam == mezszam)
                {
                    Console.WriteLine($"\tA megadott mezszámot {jatekos.Nev} viseli!");
                    talalat++;
                }
            }

            if (talalat == 0)
                Console.WriteLine("\tA megadott mezszámot egy játékos sem viseli!");
        }

        private static void F08()
        {
            Console.WriteLine("8. Feladat: Az alábbi években pontosan 3 játékos született: ");
            var evekszerint = new Dictionary<int, int>();
            foreach (var jatekos in jatekosok)
            {
                if (!evekszerint.ContainsKey(jatekos.Szulev))
                {
                    evekszerint.Add(jatekos.Szulev, 1);
                }
                else
                {
                    evekszerint[jatekos.Szulev]++;
                }
            }
            foreach (var jatekos in evekszerint)
            {
                if (jatekos.Value == 3)
                {
                    Console.WriteLine($"\t{jatekos.Key}");
                }
            }
        }

        private static void F07()
        {
            var legidosebbCsatar = jatekosok.Where(x => x.Poszt == "csatár").OrderBy(y => y.Eletkor).Last();
            Console.WriteLine($"7. Feladat: A legidősebb csatár: {legidosebbCsatar.Nev}");
        }

        private static void F06()
        {
            Console.WriteLine("6. Feladat: A csapat posztok szerint csoportosítva: ");
            var posztokSzerint = new Dictionary<string, int>();
            foreach (var jatekos in jatekosok)
            {
                if (!posztokSzerint.ContainsKey(jatekos.Poszt))
                {
                    posztokSzerint.Add(jatekos.Poszt, 1);
                }
                else
                {
                    posztokSzerint[jatekos.Poszt]++;
                }
            }

            foreach (var poszt in posztokSzerint)
            {
                Console.WriteLine($"\t{poszt.Key} - {poszt.Value} db");
            }
        }

        private static void F05()
        {
            Console.WriteLine("5. Feladat: A csapat átlagéletkora: {0:00} év!", jatekosok.Average(x => x.Eletkor));
        }

        private static void F04()
        {
            var legfiatalabb = jatekosok[0];

            foreach (var jatekos in jatekosok)
            {
                if (jatekos.Szulev > legfiatalabb.Szulev)
                {
                    legfiatalabb = jatekos;
                }
            }

            Console.WriteLine($"4. Feladat: A csapat legfiatalabb játékosa: {legfiatalabb.Nev}");
        }

        private static void F03()
        {
            var olaszok = 0;
            foreach (var jatekos in jatekosok)
            {
                if (jatekos.Nemzetiseg == "olasz")
                {
                    olaszok++;
                }
            }

            Console.WriteLine($"3. Feladat: A csapatnak {olaszok} db olasz nemzetiségű játékosa van!");
        }

        private static void F02()
        {
            var magyarok = 0;
            foreach (var jatekos in jatekosok)
            {
                if (jatekos.Nemzetiseg == "magyar")
                {
                    magyarok++;
                }
            }

            if (magyarok > 0)
            {
                Console.WriteLine("2. Feladat: Van magyar játékos!");
            }
            else
            {
                Console.WriteLine("2. Feladat: Nincs magyar játékos!");
            }
        }

        private static void F01()
        {
            Console.WriteLine($"1. Feladat: Igazolt játékosok száma: {jatekosok.Count}");
        }

        private static void Beolvasas()
        {
            using (var sr = new StreamReader(@"..\..\RES\juve.txt", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    jatekosok.Add(new Jatekos(sr.ReadLine()));
                }
            }
        }
    }
}
