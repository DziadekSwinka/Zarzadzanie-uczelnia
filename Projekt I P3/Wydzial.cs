using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projekt_I_P3
{
    internal class Wydzial
    {
        public string nazwa;
        public List<Kierunek> Kierunki= new List<Kierunek>();
        public int ID;
        static int licznikWydzialow;

        public Wydzial(string nazwa)
        {
            this.nazwa = nazwa;
            ID = licznikWydzialow++;
        }
        public void DodajKierunek(string Nazwa,int LiczbaSem)
        {
            Kierunki.Add(new Kierunek(Nazwa,LiczbaSem));
        }
        public void UsunKierunek(string Nazwa)
        {
            Kierunki.RemoveAll(x => x.nazwa == Nazwa);
        }
        public string PokazKierunkiWydzialu()
        {
            string wynik = "";
            foreach (var kierunek in Kierunki)
            {
                wynik += kierunek.PokazKierunek() + "\n";
            }
            return wynik;
        }
        public string PokazWydzialy()
        {
            string wynik = "";
            wynik +=nazwa+"\n";
            return wynik;
        }
    }
}
