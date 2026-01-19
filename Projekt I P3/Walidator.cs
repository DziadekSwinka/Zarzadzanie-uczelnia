using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projekt_I_P3
{
    internal class Walidator : IWalidator
    {
        private Uczelnia uczelnia;
        public Walidator(Uczelnia uczelnia) => this.uczelnia = uczelnia; 

        public bool SprawdzEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public bool Istnieje_Wydzial(string wydzial) 
            => uczelnia.Wydzialy.Any(w => w.nazwa.ToLower() == wydzial.ToLower());

        public bool Istnieje_Kierunek(string wydzial, string kierunek)
            => uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa.ToLower() == wydzial.ToLower())?
                .Kierunki.Any(k => k.nazwa.ToLower() == kierunek.ToLower()) ?? false;

        public bool Istnieje_Grupa(string wydzial, string kierunek, string grupa)
            => uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa.ToLower() == wydzial.ToLower())?
                .Kierunki
                .FirstOrDefault(k => k.nazwa.ToLower() == kierunek.ToLower())?
                .Grupy.Any(g =>g.nazwa.ToLower() == grupa.ToLower()) ?? false;
        
        public bool Istnieje_Przedmiot(string wydzial, string kierunek, string przedmiot)
            => uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa.ToLower() == wydzial.ToLower())?
                .Kierunki
                .FirstOrDefault(k => k.nazwa.ToLower() == kierunek.ToLower())?
                .Grupy.Any(p =>p.nazwa.ToLower() == przedmiot.ToLower()) ?? false;


        public string Wprowadz_Wydzial()
        {
            while (true) {
                Console.Write("wydzial: ");
                string? nazwa = Console.ReadLine();

                var wydzial = uczelnia.Wydzialy
                    .FirstOrDefault(w => w.nazwa.ToLower() == nazwa.ToLower());

                if (wydzial == null) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("BŁĘDNA NAZWA");
                    Console.ResetColor();
                    continue; 
                }
                else return wydzial.nazwa;

            }
        }
        public string Wprowadz_Kierunek(string wydzial)
        {
            while (true) {
                Console.Write("kierunek: ");
                string? nazwa = Console.ReadLine();

                var kierunek = uczelnia.Wydzialy
                    .FirstOrDefault(w => w.nazwa.ToLower() == wydzial.ToLower())?
                    .Kierunki
                    .FirstOrDefault(k => k.nazwa.ToLower() == nazwa.ToLower());

                if (kierunek == null) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("BŁĘDNA NAZWA");
                    Console.ResetColor();
                    continue; 
                }
                else return kierunek.nazwa;

            }
        }
        public string Wprowadz_Grupe(string wydzial,string kierunek)
        {
            while (true)
            {
                Console.Write("grupa: ");
                string? nazwa = Console.ReadLine();

                var grupa = uczelnia.Wydzialy
                    .FirstOrDefault(w => w.nazwa.ToLower() == wydzial.ToLower())?
                    .Kierunki
                    .FirstOrDefault(k=>k.nazwa.ToLower() == kierunek.ToLower())?
                    .Grupy
                    .FirstOrDefault(g => g.nazwa.ToLower() == nazwa.ToLower());

                if (kierunek == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("BŁĘDNA NAZWA");
                    Console.ResetColor();
                    continue;
                }
                else return grupa.nazwa;

            }
        }public string Wprowadz_Przedmiot(string wydzial,string kierunek)
        {
            while (true)
            {
                Console.Write("kierunek: ");
                string? nazwa = Console.ReadLine();

                var przedmiot = uczelnia.Wydzialy
                    .FirstOrDefault(w => w.nazwa.ToLower() == wydzial.ToLower())?
                    .Kierunki
                    .FirstOrDefault(k=>k.nazwa.ToLower() == kierunek.ToLower())?
                    .Przedmioty
                    .FirstOrDefault(p => p.Nazwa.ToLower() == nazwa.ToLower());

                if (kierunek == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("BŁĘDNA NAZWA");
                    Console.ResetColor();
                    continue;
                }
                else return przedmiot.Nazwa;

            }
        }



    }
}
