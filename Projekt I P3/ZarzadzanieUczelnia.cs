using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;

namespace Projekt_I_P3
{

    internal class ZarzadzanieUczelnia
    {
        public Uczelnia uczelnia = new Uczelnia();
        public Walidator walidator;

        private Wydzial? GetWydzial(string wydzial) =>
            uczelnia.Wydzialy.FirstOrDefault(w => w.nazwa == wydzial);
        private Kierunek? GetKierunek(string wydzial,string kierunek) =>
            GetWydzial(wydzial)?
                .Kierunki.FirstOrDefault(k => k.nazwa == kierunek);
        private Grupa? GetGrupe(string wydzial,string kierunek,string grupa) =>
            GetKierunek(wydzial,kierunek)?
                .Grupy.FirstOrDefault(g => g.nazwa == grupa);
        private Student? GetStudenta(string wydzial, string kierunek, string grupa, string imie, string nazwisko) {
            return GetGrupe(wydzial, kierunek, grupa)?
            .Studenci.FirstOrDefault(s =>s.Imie == imie && s.Nazwisko == nazwisko);
        }

        public ZarzadzanieUczelnia() => walidator = new Walidator(uczelnia);
        public void DodajWydzial(string Nazwa) => uczelnia.dodajWydzial(Nazwa);
        public void UsunWydzial(string Nazwa) => uczelnia.usunWydzial(Nazwa);
        public string PokazWydzialy() { return (uczelnia.PokazWydzialy()); }

        public void DodajKierunek(string Wydzial, string Kierunek, int LiczbaSem) 
        {
           GetWydzial(Wydzial)?
                .DodajKierunek(Kierunek, LiczbaSem);
        }
        public void UsunKierunek(string Wydzial, string Kierunek)
        {
            GetWydzial(Wydzial)?
                .UsunKierunek(Kierunek);
        }
        public string PokazKierunkiWydzialu(string Wydzial)
        {
            return (GetWydzial(Wydzial)
                .PokazKierunkiWydzialu());
        }
        public string PokazKierunkiNaWydzialach()
        {
            return string.Join("\n\n",
                uczelnia.Wydzialy.Select(w =>
                $"Wydział: {w.nazwa}\n" +
                    string.Join("\n",
                        w.Kierunki.Select(k => $"  - {k.nazwa}")
                    )
                )
            );
        }

        public void DodajGrupe(string Wydzial,string Kierunek,string Grupa)
        {
            GetKierunek(Wydzial,Kierunek)?
                .DodajGrupe(Grupa);
        }
        public void UsunGrupe(string Wydzial,string Kierunek,string Grupa)
        {
            GetKierunek(Wydzial, Kierunek)?
                .UsunGrupe(Grupa);
        }
        public string PokazGrupyKierunku(string Wydzial, string Kierunek)
        {
            return (string.Join("\n",
                uczelnia.Wydzialy
                .Where(w => w.nazwa == Wydzial)
                .SelectMany(w => w.Kierunki)
                .Where(k => k.nazwa == Kierunek)
                .SelectMany(k => k.Grupy)
                .Select(p => p.nazwa)));
        }
        public string PokazWszystkieGrupy()
        {
            return string.Join("\n",
                uczelnia.Wydzialy
                    .SelectMany(w => w.Kierunki)
                    .SelectMany(k => k.Grupy)
                    .Select(p => p.nazwa)
                    .Distinct()
                    .OrderBy(n => n)
            );
        }
        public string PokazGrupyNaKierunkach()
        {
            return string.Join("\n\n",
                uczelnia.Wydzialy.Select(w =>
                    $"Wydział: {w.nazwa}\n" +
                    string.Join("\n",
                        w.Kierunki.Select(k =>
                            $"  Kierunek: {k.nazwa}\n" +
                            string.Join("\n",
                                k.Grupy.Select(p => $"    - {p.nazwa}")
                            )
                        )
                    )
                )
            );
        }
        public string PokazStudentowKierunku(string Wydzial,string Kierunek)
        {
            return(GetKierunek(Wydzial, Kierunek)
                .PokazStudentowKierunku());
        }public void DodajPrzedmiot(string Wydzial,string Kierunek,string Przedmiot)
        {
            GetKierunek(Wydzial, Kierunek)?
                .DodajPrzedmiot(Przedmiot);
        }
        public void UsunPrzedmiot(string Wydzial,string Kierunek,string Przedmiot)
        {
            GetKierunek(Wydzial, Kierunek)?
                 .UsunPrzedmiot(Przedmiot);
        }
        public string PokazPrzedmiotyKierunku(string Wydzial,string Kierunek)
        {
            return (string.Join("\n",
                uczelnia.Wydzialy
                .Where(w => w.nazwa == Wydzial)
                .SelectMany(w => w.Kierunki)
                .Where(k => k.nazwa == Kierunek)
                .SelectMany(k => k.Przedmioty)
                .Select(p => p.Nazwa)));
        }
        public string PokazWszystkiePrzedmioty()
        {
            return string.Join("\n",
                uczelnia.Wydzialy
                    .SelectMany(w => w.Kierunki)
                    .SelectMany(k => k.Przedmioty)
                    .Select(p => p.Nazwa)
                    .Distinct()
                    .OrderBy(n=>n)
            );
        }
        public string PokazPrzedmiotyNaKierunkach()
        {
            return string.Join("\n\n",
                uczelnia.Wydzialy.Select(w =>
                    $"Wydział: {w.nazwa}\n" +
                    string.Join("\n",
                        w.Kierunki.Select(k =>
                            $"  Kierunek: {k.nazwa}\n" +
                            string.Join("\n",
                                k.Przedmioty.Select(p => $"    - {p.Nazwa}")
                            )
                        )
                    )
                )
            );
        }

        public void DodajStudenta(string Wydzial,string Kierunek,string Grupa,string Imie,string Nazwisko, DateOnly DataUrodzenia,string Email,int Semestr)
        {
            GetGrupe(Wydzial,Kierunek,Grupa)?
                .DodajStudenta(Imie,Nazwisko,DataUrodzenia,Email,Semestr);
        }
        public void SkreslStudenta(string Wydzial, string Kierunek, string Grupa,string Imie,string Nazwisko)
        {
            GetGrupe(Wydzial, Kierunek, Grupa)?
                .UsunStudenta(Imie,Nazwisko);
        }
        public string PokazStudenta(string Wydzial, string Kierunek, string Grupa, string Imie, string Nazwisko)
        {
            return (GetStudenta(Wydzial,Kierunek,Grupa,Imie,Nazwisko)
                .PokazStudentaExtd()/*+PokazSredniaStudenta(Wydzial,Kierunek,Grupa,Imie,Nazwisko,Semestr)*/);

        }
        public void LiczbaStudentow(string Wydzial, string Kierunek, string Grupa)
        {
            GetGrupe(Wydzial, Kierunek, Grupa)?
                .LiczbaStudentow();
        }
        public string PokazStudentowGrupy(string Wydzial, string Kierunek, string Grupa)
        {
            return(GetGrupe(Wydzial, Kierunek, Grupa)
                .PokazStudentowGrupy());
        }
        public void DodajOcene(string Wydzial, string Kierunek, string Grupa,string Imie,string Nazwisko, float Ocena, string przedmiot, DateOnly Data)
        {
            GetStudenta(Wydzial, Kierunek, Grupa, Imie, Nazwisko)?
                .DodajOcene(Ocena,przedmiot,Data);
        }
        /*public void PokazOceneStudentaZPrzedmiotu(string Wydzial, string Kierunek, string Grupa,string Imie,string Nazwisko, string przedmiot)
        {
            GetStudenta(Wydzial, Kierunek, Grupa, Imie, Nazwisko)?
                .PokazOcenyStudentaZPrzedmiotu(przedmiot);
        }*/
        public void PokazSredniaStudenta(string Wydzial, string Kierunek, string Grupa,string Imie,string Nazwisko,int Semestr)
        {
            GetStudenta(Wydzial, Kierunek, Grupa, Imie, Nazwisko)?
                .Srednia(/*Semestr*/);
        }
        public void ZmienOceneStudenta(string Wydzial,string Kierunek, string Grupa,string Imie,string Nazwisko,float NowaOcena,int IDoceny)
        {
            GetStudenta(Wydzial, Kierunek, Grupa, Imie, Nazwisko)?
                .ZmienOcene(IDoceny,NowaOcena);
        }
        public void ZaliczSemestrStudenta(string Wydzial,string Kierunek, string Grupa,string Imie,string Nazwisko)
        {
            GetStudenta(Wydzial, Kierunek, Grupa, Imie, Nazwisko)?
                .ZaliczSemestr();
        }
        public void PokazOcenyStudentaZPrzedmiotu(string Wydzial, string Kierunek, string Grupa, string Imie, string Nazwisko,string Przedmiot)
        {
            GetStudenta(Wydzial, Kierunek, Grupa, Imie, Nazwisko)?
                .PokazOcenyStudentaZPrzedmiotu(Przedmiot);
        }
        public int LiczbaStudentow()
        {
            return uczelnia.Wydzialy
                .SelectMany(w => w.Kierunki)
                .SelectMany(k => k.Grupy)
                .SelectMany(g => g.Studenci)
                .Count();
        }
        public int LiczbaPrzedmiotow()
        {
            return uczelnia.Wydzialy
                .SelectMany(w => w.Kierunki)
                .SelectMany(k => k.Przedmioty)
                .Count();
        }
       public string LiczbaStudentowKierunki()
       {
            return string.Join("\n\n",
                uczelnia.Wydzialy.Select(w =>
                    $"Wydział: {w.nazwa}\n" +
                    string.Join("\n",
                        w.Kierunki.Select(k =>
                            $"  Kierunek: {k.nazwa}: " +
                            k.Grupy.Sum(g => g.Studenci.Count)
                        )
                    )
                )
            );
       }
        private string Zlam(string s)
        {
            return s.Replace(" ", "\n");
        }
        public void RysujWykres()
        {
            double[] values= uczelnia.Wydzialy
                .SelectMany(w => w.Kierunki)
                .Select(k => (double)k.Grupy.Sum(g => g.Studenci.Count))
                .ToArray();

            string[] labels = uczelnia.Wydzialy
                .SelectMany(w => w.Kierunki)
                .Select(k => Zlam(k.nazwa))
                .ToArray();

            double[] xs = Enumerable.Range(0, values.Length)
                            .Select(i => (double)i)
                            .ToArray();

            var plt = new ScottPlot.Plot();

            plt.Add.Bars(xs,values);
            plt.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                Enumerable.Range(0, labels.Length).Select(i => (double)i).ToArray(),
                labels
            );
            plt.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic()
            {
                IntegerTicksOnly = true
            };

            //plt.Axes.Bottom.TickLabelStyle.Rotation = 15;
            plt.Axes.Bottom.TickLabelStyle.FontSize = 16;
            plt.Axes.Bottom.TickLabelStyle.Alignment = ScottPlot.Alignment.UpperCenter;

            plt.Axes.Margins(bottom: 0);

            plt.SavePng($"wykres.png",1920,1080);

        }

    }
}

