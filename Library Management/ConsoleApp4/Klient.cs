
using System;

namespace Ksiegarnia
{
    public class Klient
    {
        public int id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int id_ks = 0;
        public int saldo = 0;

        internal static Klient ParsujCSV(string linia)
        {
            var kolumny = linia.Split(',');

            return new Klient
            {
                id = int.Parse(kolumny[0]),
                Imie = kolumny[1],
                Nazwisko = kolumny[2],
            };
        }
    }
}
