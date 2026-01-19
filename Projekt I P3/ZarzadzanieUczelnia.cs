using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScottPlot;

namespace Projekt_I_P3
{
    internal class ZarzadzanieUczelnia
    {
        public Uczelnia uczelnia = new Uczelnia();
        public Walidator walidator;
        public ZarzadzanieUczelnia() => walidator = new Walidator(uczelnia);
        public void DodajWydzial(string Nazwa) => uczelnia.dodajWydzial(Nazwa);
        public void UsunWydzial(string Nazwa) => uczelnia.usunWydzial(Nazwa);
        public string PokazWydzialy() { return (uczelnia.PokazWydzialy()); }

        public void DodajKierunek(string Wydzial, string Kierunek, int LiczbaSem) 
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .dodajKierunek(Kierunek, LiczbaSem);
        }
        public void UsunKierunek(string Wydzial, string Kierunek)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .UsunKierunek(Kierunek);
        }
        public string PokazKierunkiWydzialu(string Wydzial)
        {
            return (uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
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
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .DodajGrupe(Grupa);
        }
        public void UsunGrupe(string Wydzial,string Kierunek,string Grupa)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
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
            return(uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .PokazStudentowKierunku());
        }public void DodajPrzedmiot(string Wydzial,string Kierunek,string Przedmiot)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .DodajPrzedmiot(Przedmiot);
        }
        public void UsunPrzedmiot(string Wydzial,string Kierunek,string Przedmiot)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
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
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .DodajStudenta(Imie,Nazwisko,DataUrodzenia,Email,Semestr);
        }
        public void SkreslStudenta(string Wydzial, string Kierunek, string Grupa,string Imie,string Nazwisko)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .UsunStudenta(Imie,Nazwisko);
        }
        public string PokazStudenta(string Wydzial, string Kierunek, string Grupa, string Imie, string Nazwisko)
        {
            return (uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .Studenci
                .FirstOrDefault(i => i.Imie == Imie &&  i.Nazwisko == Nazwisko)?
                .pokazStudentaExtd()/*+PokazSredniaStudenta(Wydzial,Kierunek,Grupa,Imie,Nazwisko,Semestr)*/);

        }
        public void LiczbaStudentow(string Wydzial, string Kierunek, string Grupa)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .LiczbaStudentow();
        }
        public string PokazStudentowGrupy(string Wydzial, string Kierunek, string Grupa)
        {
            return(uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .PokazStudentowGrupy());
        }
        public void DodajOcene(string Wydzial, string Kierunek, string Grupa,string Imie,string Nazwisko, float Ocena, string przedmiot, DateOnly Data)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .Studenci
                .FirstOrDefault(s => s.Imie == Imie && s.Nazwisko == Nazwisko)?
                .DodajOcene(Ocena,przedmiot,Data);
        }
        public void PokazOceneStudentaZPrzedmiotu(string Wydzial, string Kierunek, string Grupa,string Imie,string Nazwisko, string przedmiot)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .Studenci
                .FirstOrDefault(s => s.Imie == Imie && s.Nazwisko == Nazwisko)?
                .PokazOcenyStudentaZPrzedmiotu(przedmiot);
        }
        public void PokazSredniaStudenta(string Wydzial, string Kierunek, string Grupa,string Imie,string Nazwisko,int Semestr)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .Studenci
                .FirstOrDefault(s => s.Imie == Imie && s.Nazwisko == Nazwisko)?
                .Srednia(/*Semestr*/);
        }
        public void ZmienOceneStudenta(string Wydzial,string Kierunek, string Grupa,string Imie,string Nazwisko,float NowaOcena,int IDoceny)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .Studenci
                .FirstOrDefault(s => s.Imie == Imie && s.Nazwisko == Nazwisko)?
                .ZmienOcene(IDoceny,NowaOcena);
        }
        public void ZaliczSemetrStudenta(string Wydzial,string Kierunek, string Grupa,string Imie,string Nazwisko)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .Studenci
                .FirstOrDefault(s => s.Imie == Imie && s.Nazwisko == Nazwisko)?
                .ZaliczSemestr();
        }
        public void PokazOcenyStudentaZPrzedmiotu(string Wydzial, string Kierunek, string Grupa, string Imie, string Nazwisko,string Przedmiot)
        {
            uczelnia.Wydzialy
                .FirstOrDefault(w => w.nazwa == Wydzial)?
                .Kierunki
                .FirstOrDefault(k => k.nazwa == Kierunek)?
                .Grupy
                .FirstOrDefault(g => g.nazwa == Grupa)?
                .Studenci
                .FirstOrDefault(s => s.Imie == Imie && s.Nazwisko == Nazwisko)?
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
        /*public string StudenciZNajwyzszaSrednia(int Top)
        {
            var studenci = uczelnia.Wydzialy
                .SelectMany(w => w.Kierunki)
                .SelectMany(k => k.Grupy)
                .SelectMany(g => g.Studenci)
                .Where(s => s.Srednia() > 0)
                .OrderByDescending(s => s.Srednia())
                .ToList();

            var top = studenci
                .Select(s => s.Srednia())
                .Distinct()
                .Take(Top)
                .ToList();

            return string.Join("\n",
            studenci
            .Where(s => top.Contains(s.Srednia()))
            .Select((s, i) =>
                $"{i + 1}. {s.Imie} {s.Nazwisko} – {s.Srednia():F2}"
            ));  
        }*/
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
        public void RysujWykrs()
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
            plt.Axes.Bottom.TickLabelStyle.Alignment = ScottPlot.Alignment.UpperCenter;

            plt.Axes.Margins(bottom: 0);

            plt.SavePng($"wykres.png",1920,1080);

        }

    }
}

