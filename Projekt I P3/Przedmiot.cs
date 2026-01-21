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
            return wynik;
        }

    }
}
