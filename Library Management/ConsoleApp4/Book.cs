
using System;

namespace Ksiegarnia
{
    public class Book
    {
        public int id { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public int availabilty = 1;

        internal static Book ParsujCSV(string linia)
        {
            var kolumny = linia.Split(',');

            return new Book
            {
                id = int.Parse(kolumny[0]),
                author = kolumny[1],
                title = kolumny[2],
            };
        }
    }
}
