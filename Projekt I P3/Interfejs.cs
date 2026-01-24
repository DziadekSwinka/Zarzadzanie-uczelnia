using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projekt_I_P3
{
    internal class Interfejs
    {
        enum Stan { MenuGlowne, Wydzialy, Kierunki, Grupy, Studenci, Przedmioty, Statystyki, None}
        private Stan stan= Stan.MenuGlowne;
        private string input = ""; 

        private readonly ZarzadzanieUczelnia uczelnia;
        private readonly IWalidator walidator;
        private readonly KomunikatyUzytkownika komunikaty;
        public Interfejs(ZarzadzanieUczelnia uczelnia, IWalidator walidator, KomunikatyUzytkownika komunikaty)  
        { 
            this.uczelnia = uczelnia; 
            this.walidator = walidator; 
            this.komunikaty = komunikaty; 
        }
        private (string, string) WK()
        {
            Console.Write("wydział: ");
            string w = uczelnia.walidator.Wprowadz_Wydzial();
            Console.Write("kierunrk: ");
            string k = uczelnia.walidator.Wprowadz_Kierunek(w);
            return (w, k);
        }
        private (string,string,string) WKG()
        {
            var (w,k) = WK();
            string g = uczelnia.walidator.Wprowadz_Grupe(w,k);
            return (w, k, g);
        }
        private (string,string,string) WKP()
        {
            var (w,k) = WK();
            string p = uczelnia.walidator.Wprowadz_Przedmiot(w,k);
            return (w, k, p);
        }
        private void Header(string s)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"{s}\n");
            Console.ResetColor();
        }
        private bool Potwierdz(string tresc)
        {
            Console.Write($"{tresc} [Y/N]: ");
            string o = Console.ReadLine()?.ToLower();
            return o == "y";
        }

        public bool Update()
        {
            Console.Clear();
            switch(stan)
            {
                case Stan.MenuGlowne: MenuGlowne(); break;
                case Stan.Wydzialy: Wydzialy(); break;
                case Stan.Kierunki: Kierunek(); break;
                case Stan.Grupy: Grupa(); break;
                case Stan.Przedmioty: Przedmiot(); break;
                case Stan.Studenci: Student(); break;
                case Stan.Statystyki: Statystyki(); break;
                case Stan.None: return false;
            }
            return true;
        }
        public void MenuGlowne()
        {
            Header("System Zarządzania Uczelnią");
            Console.WriteLine("[1]\tWydziały");
            Console.WriteLine("[2]\tKierunki");
            Console.WriteLine("[3]\tGrupy");
            Console.WriteLine("[4]\tStudenci");
            Console.WriteLine("[5]\tPrzedmioty");
            Console.WriteLine("[6]\tStatystyki");
            Console.WriteLine("\n[0]\tWyjście");
            input = Console.ReadLine();
            switch (input)
            {
                case "1": stan = Stan.Wydzialy; break; 
                case "2": stan = Stan.Kierunki; break; 
                case "3": stan = Stan.Grupy; break; 
                case "4": stan = Stan.Studenci; break; 
                case "5": stan = Stan.Przedmioty; break; 
                case "6": stan = Stan.Statystyki; break; 
                case "0": stan = Stan.None; break; 
            }
        }
        public void Wydzialy()
        {
            Header("Wydzdiały uczelni");
            Console.WriteLine("[1]\tLista wydziałów");
            Console.WriteLine("[2]\tDodaj wydział");
            Console.WriteLine("[3]\tUsuń wydział");
            Console.WriteLine("\n[0]\tPowrót");

            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        Console.Clear();
                        Console.Write(uczelnia.PokazWydzialy());
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        Console.Write("wydział: ");
                        string w= Console.ReadLine();
                        if (!uczelnia.walidator.Istnieje_Wydzial(w))
                            uczelnia.DodajWydzial(w);
                        else komunikaty.Error_NazwaJuzZajeta();
                        break;
                    }
                case "3":
                    {
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        if(Potwierdz($"Czy napewno chcesz usunąć {w}?"))
                            uczelnia.UsunWydzial(w);
                        break;
                    }
                case "0": stan= Stan.MenuGlowne; break;
            }
        }
        public void Kierunek()
        {
            Header("Kierunki");
            Console.WriteLine("[1]\tLista kierunków");
            Console.WriteLine("[2]\tLista kierunków z wydziału");
            Console.WriteLine("[3]\tDodaj kierunek");
            Console.WriteLine("[4]\tUsuń kierunek");
            Console.WriteLine("\n[0]\tPowrót");

            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        Console.Clear();
                        Console.Write(uczelnia.PokazKierunkiNaWydzialach());
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        Console.Clear();
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        Console.Write(uczelnia.PokazKierunkiWydzialu(w));
                        Console.ReadKey();
                        break;
                    }
                case "3": 
                    {
                        Console.Write("kierunrk: ");
                        string k = Console.ReadLine();
                        Console.Write("liczba semestrów: ");
                        int ls = Convert.ToInt32(Console.ReadLine());
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        if(!uczelnia.walidator.Istnieje_Kierunek(w,k))
                            uczelnia.DodajKierunek(w,k,ls);
                        else komunikaty.Error_NazwaJuzZajeta();
                        break; 
                    }
                case "4":
                    {
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        if (Potwierdz($"Czy napewno chcesz usunąć {k}?"))
                            uczelnia.UsunKierunek(w,k);
                        break;
                    }
                case "0": stan = Stan.MenuGlowne; break;
            }

        }
        public void Student()
        {
            Header("Studenci");
            Console.WriteLine("[1]\tLista Studentów Kierunku");
            Console.WriteLine("[2]\tLista Studentów Grupy");
            Console.WriteLine("[3]\tDodaj Studenta");
            Console.WriteLine("[4]\tSkreśl Studenta");
            Console.WriteLine("[5]\tSzczegóły Studenta i OCENY");
            Console.WriteLine("\n[0]\tPowrót");

            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        var (w, k) = WK();
                        Console.Write(uczelnia.PokazStudentowKierunku(w, k));
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        var (w, k, g) = WKG();
                        Console.Write(uczelnia.PokazStudentowGrupy(w,k,g));
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        Console.Write("imię: ");
                        string i = Console.ReadLine();
                        Console.Write("nazwisko: ");
                        string n = Console.ReadLine();
                        Console.Write("data urodzenia [dd-mm-yyyy]: ");
                        DateOnly d = DateOnly.Parse(Console.ReadLine());
                        Console.Write("email: ");
                        string e = Console.ReadLine();
                        var (w, k, g) = WKG();
                        Console.Write("semestr: ");                           
                        int s = Convert.ToInt32(Console.ReadLine());
   
                        if(uczelnia.walidator.SprawdzEmail(e))
                            uczelnia.DodajStudenta(w,k,g,i,n,d,e,s);
                        break;
                    }
                case "4":
                    {
                        Console.Write("imię: ");
                        string i = Console.ReadLine();
                        Console.Write("nazwisko: ");
                        string n = Console.ReadLine();
                        var (w, k, g) = WKG();
                        if (Potwierdz($"Czy napewno chcesz skreślić studenta {i} {n}?"))
                            uczelnia.SkreslStudenta(w,k,g,i,n);
                        break;
                    }
                case "5":
                    {
                        Console.Write("imię: ");
                        string i = Console.ReadLine();
                        Console.Write("nazwisko: ");
                        string n = Console.ReadLine();
                        var (w, k, g) = WKG();
                        Console.Write("\n\n");
                        Console.WriteLine("[1]\tDodaj ocenę");
                        Console.WriteLine("[2]\tDodaj ocenę");
                        Console.WriteLine("[3]\tZalicz semestr");
                        Console.WriteLine("[0]\tPowrót");
                        Console.Write(uczelnia.PokazStudenta(w, k, g, i, n));

                        input = Console.ReadLine();
                        switch(input)
                        {
                            case "1": {
                                    Console.Write("ocena: ");
                                    float o = float.Parse(Console.ReadLine());
                                    string p = uczelnia.walidator.Wprowadz_Przedmiot(w,k);
                                    uczelnia.DodajOcene(w,k,g,i,n,o,p, DateOnly.FromDateTime(DateTime.Today));
                                    Console.Write(uczelnia.PokazStudenta(w, k, g, i, n));
                                    break; 
                                }
                            case "2":
                                {
                                    Console.Write("nowa ocena: ");
                                    float o = float.Parse(Console.ReadLine());
                                    Console.Write("ID oceny: ");
                                    int id = Convert.ToInt32(Console.ReadLine());
                                    uczelnia.ZmienOceneStudenta(w,k,g,i,n,o,id);
                                    break;
                                }
                            case "3":
                                {
                                    uczelnia.ZaliczSemetrStudenta(w,k,g,i,n);
                                    break;
                                }
                            case "0": stan = Stan.MenuGlowne; break;
                        }
                        break;
                    }
                case "0": stan = Stan.MenuGlowne; break;
            }
        }

        public void Grupa()
        {
            Header("Grupy");
            Console.WriteLine("[1]\tLista Grup (Drzewo)");
            Console.WriteLine("[2]\tLista Grup (Lista)");
            Console.WriteLine("[3]\tLista Grup Kierunku");
            Console.WriteLine("[4]\tDodaj Grupę");
            Console.WriteLine("[5]\tUsun Grupę");
            Console.WriteLine("\n[0]\tPowrót");

            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        Console.Clear();
                        Console.WriteLine(uczelnia.PokazGrupyNaKierunkach());
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        Console.Clear();
                        Console.WriteLine(uczelnia.PokazWszystkieGrupy());
                        Console.ReadKey();
                        break;
                    }
                case "3":
                    {
                        Console.Clear();
                        var (w, k) = WK();
                        Console.WriteLine(uczelnia.PokazGrupyKierunku(w, k));
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        Console.Write("nazwa: ");
                        string g = Console.ReadLine();
                        var (w, k) = WK();
                        if(!uczelnia.walidator.Istnieje_Grupa(w,k,g))
                            uczelnia.DodajGrupe(w, k, g);
                        else komunikaty.Error_NazwaJuzZajeta();
                        break;
                    }
                case "5":
                    {
                        Console.Write("nazwa: ");
                        string n = Console.ReadLine();
                        var (w, k) = WK();
                        uczelnia.UsunGrupe(w, k, n);
                        break;
                    }

                case "0": stan = Stan.MenuGlowne; break;
            }
        }
        public void Przedmiot()
        {
            Header("Przedmioty");
            Console.WriteLine("[1]\tLista Przedmiotów (Drzewo)");
            Console.WriteLine("[2]\tLista Przedmiotów (Lista)");
            Console.WriteLine("[3]\tLista Przedmiotów Kierunku");
            Console.WriteLine("[4]\tDodaj Przedmiot");
            Console.WriteLine("[5]\tUsun Przedmiot");
            Console.WriteLine("\n[0]\tPowrót");

            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    {
                        Console.Clear();
                        Console.WriteLine(uczelnia.PokazPrzedmiotyNaKierunkach());
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        Console.Clear();
                        Console.WriteLine(uczelnia.PokazWszystkiePrzedmioty());
                        Console.ReadKey();
                        break;
                    }                
                case "3":
                    {
                        Console.Clear();
                        var (w, k) = WK();
                        Console.WriteLine(uczelnia.PokazPrzedmiotyKierunku(w,k));
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        Console.Write("przedmiot: ");
                        string p = Console.ReadLine();
                        var (w, k) = WK();
                        if(!uczelnia.walidator.Istnieje_Przedmiot(w,k,p))
                            uczelnia.DodajPrzedmiot(w,k,p);
                        else komunikaty.Error_NazwaJuzZajeta();
                        break;
                    }
                case "5":
                    {
                        var (w, k, p) = WKP();
                        if (Potwierdz($"Czy napewno chcesz usunąć {p} dla kierunku {k}?"))
                            uczelnia.UsunPrzedmiot(w,k,p);
                        break;
                    }
                case "0": stan = Stan.MenuGlowne; break;
            }
        }
        public void Statystyki()
        {
            Header("Statystyki");
            Console.WriteLine($"Liczba przedmiotów: {uczelnia.LiczbaPrzedmiotow()}");
            Console.WriteLine($"Liczba studentów na uczelni: {uczelnia.LiczbaStudentow()}");
            Console.WriteLine($"\n\nLiczba studentów na kierunkach: \n{uczelnia.LiczbaStudentowKierunki()}");
            Console.WriteLine("\n[0]\tPowrót");
            uczelnia.RysujWykrs();
            switch (Console.ReadLine())
            {
                case "0": stan = Stan.MenuGlowne; break;
            }
        }
    }
}
