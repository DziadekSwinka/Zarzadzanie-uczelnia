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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("System Zarządzania Uczelnią\n");
            Console.ResetColor();
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Wydziały Uczelni\n");
            Console.ResetColor();
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
                        Console.Write($"Czy napewno chcesz usunąć {w}? [Y/N]\n");
                        if(Console.ReadLine()=="Y" || Console.ReadLine() == "y")
                            uczelnia.UsunWydzial(w);
                        break;
                    }
                case "0": stan= Stan.MenuGlowne; break;
            }
        }
        public void Kierunek()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Kierunki Uczelni\n");
            Console.ResetColor();
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
                        Console.Write($"Czy napewno chcesz usunąć {k}? [Y/N]\n");
                        if (Console.ReadLine() == "Y" || Console.ReadLine() == "y")
                            uczelnia.UsunKierunek(w,k);
                        break;
                    }
                case "0": stan = Stan.MenuGlowne; break;
            }

        }
        public void Student()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Studenci\n");
            Console.ResetColor();
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
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        Console.Write(uczelnia.PokazStudentowKierunku(w, k));
                        Console.ReadKey();
                        break;
                    }
                case "2":
                    {
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        string g = uczelnia.walidator.Wprowadz_Grupe(w, k);
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
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        string g = uczelnia.walidator.Wprowadz_Grupe(w, k);
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
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        string g = uczelnia.walidator.Wprowadz_Grupe(w, k);
                        Console.Write($"Czy napewno chcesz skreślić studenta {i} {n}? [Y/N]\n");
                        if (Console.ReadLine() == "Y" || Console.ReadLine() == "y")
                            uczelnia.SkreslStudenta(w,k,g,i,n);
                        break;
                    }
                case "5":
                    {
                        Console.Write("imię: ");
                        string i = Console.ReadLine();
                        Console.Write("nazwisko: ");
                        string n = Console.ReadLine();
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        string g = uczelnia.walidator.Wprowadz_Grupe(w, k);
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Grupy\n");
            Console.ResetColor();
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
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        Console.WriteLine(uczelnia.PokazGrupyKierunku(w, k));
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        Console.Write("nazwa: ");
                        string g = Console.ReadLine();
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        if(!uczelnia.walidator.Istnieje_Grupa(w,k,g))
                            uczelnia.DodajGrupe(w, k, g);
                        else komunikaty.Error_NazwaJuzZajeta();
                        break;
                    }
                case "5":
                    {
                        Console.Write("nazwa: ");
                        string n = Console.ReadLine();
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        uczelnia.UsunGrupe(w, k, n);
                        break;
                    }

                case "0": stan = Stan.MenuGlowne; break;
            }
        }
        public void Przedmiot()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Przedmioty\n");
            Console.ResetColor();
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
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        Console.WriteLine(uczelnia.PokazPrzedmiotyKierunku(w,k));
                        Console.ReadKey();
                        break;
                    }
                case "4":
                    {
                        Console.Write("przedmiot: ");
                        string p = Console.ReadLine();
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        if(!uczelnia.walidator.Istnieje_Przedmiot(w,k,p))
                            uczelnia.DodajPrzedmiot(w,k,p);
                        else komunikaty.Error_NazwaJuzZajeta();
                        break;
                    }
                case "5":
                    {
                        string w = uczelnia.walidator.Wprowadz_Wydzial();
                        string k = uczelnia.walidator.Wprowadz_Kierunek(w);
                        string p = uczelnia.walidator.Wprowadz_Przedmiot(w, k);
                        Console.Write($"Czy napewno chcesz usunąć {p}? [Y/N]\n");
                        if (Console.ReadLine() == "Y" || Console.ReadLine() == "y")
                            uczelnia.UsunPrzedmiot(w,k,p);
                        break;
                    }
                case "0": stan = Stan.MenuGlowne; break;
            }
        }
        public void Statystyki()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Statystyki\n");
            Console.ResetColor();
            Console.WriteLine($"Liczba przedmiotów: {uczelnia.LiczbaPrzedmiotow()}");
            Console.WriteLine($"Liczba studentów na uczelni: {uczelnia.LiczbaStudentow()}");
            Console.WriteLine($"\n\nLiczba studentów na kierunkach: \n{uczelnia.LiczbaStudentowKierunki()}");
            //Console.WriteLine($"Student z najwyższą średnią: {uczelnia.StudenciZNajwyzszaSrednia(1)}");
            Console.WriteLine("\n[0]\tPowrót");
            uczelnia.RysujWykrs();
            switch (Console.ReadLine())
            {
                case "0": stan = Stan.MenuGlowne; break;
            }
        }
    }
}
