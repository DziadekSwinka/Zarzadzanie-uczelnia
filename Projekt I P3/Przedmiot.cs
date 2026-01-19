using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_I_P3
{
    internal class Przedmiot
    {
        public string Nazwa;
        public int Semestr;
        public bool Egzamin;
        public int ETCS;

        private static int LicznikPrzedmiotow;
        public int ID;
        public Przedmiot(string nazwa) 
        {
            this.Nazwa = nazwa;
            ID = LicznikPrzedmiotow++;
        }
        public string PokazPrzedmiot()
        {
            string wynik="";
            wynik += Nazwa+" ";
            wynik += Semestr+" ";
            wynik += ETCS+" ";
            if (Egzamin)
                wynik += "Egzamin";
            return wynik;
        }

    }
}
