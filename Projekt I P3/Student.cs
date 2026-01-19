using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_I_P3
{
    internal class Student
    {
        public string Imie;
        public string Nazwisko;
        public int NumerIndexu;
        private static int LicznikIndexow;

        private bool AktywnyStudent;

        public DateOnly DataUrodzenia;
        public string email;
        private int semestr;

        public string grupa;
        int liczbaSemestrow;

        public List<Ocena> Oceny = new List<Ocena>();

        public Student(string Imie,string Nazwisko, DateOnly DataUrodzenia, string email, int semestr,string g,int liczbaSemestrow)
        {
            AktywnyStudent = true;
            this.Imie = Imie;
            this.Nazwisko= Nazwisko;
            this.DataUrodzenia = DataUrodzenia;
            this.email = email;
            this.semestr = semestr;
            this.grupa = g;
            this.liczbaSemestrow = liczbaSemestrow;

            this.NumerIndexu = LicznikIndexow;
            LicznikIndexow++;
           // this.NumerIndexu = Stdunci.Max( n => n.NrIndexu) + 1;
        }
        public void DodajOcene(float nowaOcena,string przedmiot,DateOnly d) 
        {
            Oceny.Add(new Ocena(nowaOcena,this.semestr,przedmiot,d));
        }
        public float Srednia(/*int Semestr*/) 
        {
            if (Oceny.Count > 0)
            {
                float avg = 0;
                int count = 0;
                float suma = 0;
                foreach (var o in Oceny)
                    //if(o.Semestr==Semestr)
                    {
                        suma += o.Wartosc;
                        count++;
                    }    
                avg = suma / count;
                return avg;
            }
            else return 0;
        }
        public void ZmienOcene(int Id, float NowaOcena) 
        {
            var index = Oceny.FindIndex(x => x.ID == Id);
            Oceny[index].Wartosc = NowaOcena;
        }
        public void ZaliczSemestr() 
        {
            if(semestr++==this.liczbaSemestrow)
                AktywnyStudent=false;
        }
        public string pokazStudenta()
        {
            string wynik="";
            wynik += "imię: "+Imie+"\n";
            wynik += "nazwisko: "+Nazwisko + "\n";
            wynik += "data urodzenia: "+DataUrodzenia + "\n";
            wynik += "numer indeksu: "+NumerIndexu + "\n";
            return wynik;
        }
        public string pokazStudentaExtd()
        {
            string wynik = "";
            wynik += pokazStudenta();
            wynik += "\noceny: \n";
            foreach (var o in Oceny)
            {
                wynik += o.Wartosc+" ";
                wynik += o.przedmiot+" ";
                wynik += o.dataUzyskania+" ";
                wynik += "ID: " + o.ID + " ";
                wynik += "\n";
            }
            return wynik;
        }
        public string pokazOcenyStudenta()
        {
            string wynik = "";
            foreach (Ocena ocena in Oceny)
                wynik += ocena.pokazOcene() + "\n";
            return wynik;
        }
        public string PokazOcenyStudentaZPrzedmiotu(string nazwa)
        {
            return string.Join("\n",
                Oceny
                    .Where(o => o.przedmiot == nazwa)
                    .Select(o => o.pokazOcene())
            );
        }

    }
}
