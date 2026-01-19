using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_I_P3
{
    internal class Ocena
    {
        public string przedmiot;
        public float Wartosc;
        public int Semestr;
        public int ID;
        private static int LicznikOcen;
        public DateOnly dataUzyskania;

        public Ocena(float Wartosc,int Semestr,string przedmiot,DateOnly dataU)
        {
            this.Semestr = Semestr;
            this.przedmiot = przedmiot;
            this.dataUzyskania = dataU;
            this.Wartosc = Wartosc;

            ID = LicznikOcen++;
        }
        public string pokazOcene()
        {
            string wynik = "";
            wynik += Wartosc+" ";
            wynik += ID+" ";
            wynik += Semestr+" ";
            wynik += dataUzyskania+" ";
            return wynik;
        }
    }
}
