using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_I_P3
{
    internal class Uczelnia
    {
        public List<Wydzial> Wydzialy = new List<Wydzial>();
        public int ID;
        public void dodajWydzial(string Nazwa)
        {
            Wydzialy.Add(new Wydzial(Nazwa));
        }
        public void usunWydzial(string Nazwa)
        {
            Wydzialy.RemoveAll(x => x.nazwa == Nazwa);
        }
        public string PokazWydzialy()
        {
            string wynik = "";
            foreach (var wydzial in Wydzialy)
            {
                wynik += wydzial.PokazWydzialy() + "\n";
            }
            return wynik;
        }
    }
}
