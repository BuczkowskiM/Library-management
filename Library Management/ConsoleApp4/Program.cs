using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ksiegarnia
{
    class Program
    {
        static void Main(string[] args)
        {
            var books = WczytywaniePliku("ksiazki.csv");
            var clients = WczytywaniePliku2("klienci.csv");

            int ending = 0;

            Console.WriteLine("Welcome to library management!");
            do
            {
                Console.WriteLine("CHOOSE OPTION:");
                Console.WriteLine("1: View all books");
                Console.WriteLine("2: View available books");
                Console.WriteLine("3: View all clients");
                Console.WriteLine("4: View clients with books");
                Console.WriteLine("5: View debtors");
                Console.WriteLine("");
                Console.WriteLine("6: Borrow a book");
                Console.WriteLine("7: Give back a book");
                Console.WriteLine("8: Change clients balance");

                string x = Console.ReadLine();
                switch (x)
                {
                    case "1": WyswietlKsiazki(books); break;
                    case "2": WyswietlDostepne(books); break;
                    case "3": WyswietlKlientow(clients); break;
                    case "4": WyswietlKlientowzKs(clients); break;
                    case "5": WyswietlDluznikow(clients); break;
                    case "6": Wypozycz(books); Wypozycz2(clients); break;
                    case "7": Oddaj(books); Oddaj2(clients); break;
                    case "8": ZmienSaldo(clients); break;
                    default: Console.WriteLine("Bad option!"); break;
                }

                Console.WriteLine("If you want to quit, press x, if not - press enter");
                var end = (Console.ReadLine());
                if (end == "x") ending++;

            } while (ending == 0);
        }

        private static List<Book> WczytywaniePliku(string sciezka)
        {
            return File.ReadAllLines(sciezka)
                       .Skip(1)
                       .Where(l => l.Length > 1)
                       .Select(Book.ParsujCSV).ToList();
        }
        private static List<Klient> WczytywaniePliku2(string sciezka2)
        {
            return File.ReadAllLines(sciezka2)
                       .Skip(1)
                       .Where(l => l.Length > 1)
                       .Select(Klient.ParsujCSV).ToList();
        }
        private static void ZmienSaldo(List<Klient> clients)
        {
            Console.WriteLine("Enter customer id:");
            int id_wypozyczajacego = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the amount of the balance change:");
            int zmianasalda = int.Parse(Console.ReadLine());

            foreach (var klient in clients)
            {
                if (klient.id == id_wypozyczajacego)
                { klient.saldo = klient.saldo + zmianasalda; }
            }
        }
        private static void WyswietlDluznikow(List<Klient> clients)
        {
            int d = 0;
            foreach (var klient in clients)
            {
                if (klient.saldo < 0)
                {
                    Console.WriteLine(klient.id + " " + klient.Imie + " " + klient.Nazwisko + " " + klient.saldo);
                    d++;
                }
            }
            if (d == 0)
                Console.WriteLine("No debtors!");
        }
        private static void WyswietlKlientowzKs(List<Klient> clients)
        {
            int d = 0;
            foreach (var klient in clients)
            {
                if (klient.id_ks != 0)
                {
                    Console.WriteLine(klient.Imie + " " + klient.Nazwisko + " " + klient.id_ks);
                    d++;
                }
            }
            if(d==0)
                Console.WriteLine("No borrowed books!");
        }
        private static void Wypozycz(List<Book> books)
        {
            Console.WriteLine("Enter book id:");
            int id_wypozyczanej = int.Parse(Console.ReadLine());
            
            foreach (var book in books)
            {
                if (book.id == id_wypozyczanej && book.availabilty == 1)
                { book.availabilty = 0;}
                else if(book.id == id_wypozyczanej && book.availabilty == 0)
                { Console.WriteLine("Book already taken!"); }
            }
        }
        private static void Wypozycz2(List<Klient> clients)
        {
            Console.WriteLine("Confirm book id:");
            int id_wypozyczanej = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter client id:");
            int id_wypozyczajacego = int.Parse(Console.ReadLine());
            
            foreach (var klient in clients)
            {
                if(id_wypozyczanej>6 || id_wypozyczanej<1)
                { Console.WriteLine("We dont have that book!"); }
                else if (klient.id == id_wypozyczajacego)
                { klient.id_ks = id_wypozyczanej; }
            }
        }
        private static void Oddaj(List<Book> books)
        {
            Console.WriteLine("Enter book id:");
            int id_wypozyczanej = int.Parse(Console.ReadLine());

            foreach (var book in books)
            {
                if (book.id == id_wypozyczanej && book.availabilty == 0)
                { book.availabilty = 1; }
                else if(book.id == id_wypozyczanej && book.availabilty == 1)
                { Console.WriteLine("Book is available!"); }
            }
        }
        private static void Oddaj2(List<Klient> clients)
        {
            Console.WriteLine("Enter client id:");
            int id_wypozyczajacego = int.Parse(Console.ReadLine());

            foreach (var klient in clients)
            {
                if (klient.id == id_wypozyczajacego)
                { klient.id_ks = 0; }
            }
        }
        private static void WyswietlDostepne(List<Book> books)
        {
            int d = 0;

            foreach (var book in books)
            {
                if (book.availabilty == 1)
                {
                    Console.WriteLine(book.id + " " + book.author + " " + book.title);
                    d++;
                }
            }

            if(d==0)
            { Console.WriteLine("Brak dostepnych ksiazek!"); }
        }
        private static void WyswietlKlientow(List<Klient> clients)
        {
            foreach (var klient in clients)
            {
                Console.WriteLine(klient.id + " " + klient.Imie + " " + klient.Nazwisko);
            }
        }
        private static void WyswietlKsiazki(List<Book> books)
        {
            foreach (var book in books)
            {
                Console.WriteLine(book.id + " " + book.author + " " + book.title);
            }
        }

    }
}
