using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projekt_I_P3
{
    internal class Grupa
    {
        public int ID;
        private static int nextID;

        public string nazwa;
        public List<Student> Studenci = new List<Student>();

        private int semestryKierunku;
        public Grupa(string nazwa,int semestryKierunku)
        {
            this.nazwa = nazwa;
            ID = nextID; nextID++;
            this.semestryKierunku= semestryKierunku;
        }
        public void DodajStudenta(string Imie,string Nazwisko,DateOnly DataUrodzenia, string Email, int Semestr) 
        {
            Studenci.Add(new Student(Imie,Nazwisko,DataUrodzenia,Email,Semestr,nazwa, semestryKierunku));
        }
        public void UsunStudenta(int id)
        {
            Studenci.RemoveAt(id);
        }
        public void UsunStudenta(string Imie, string Nazwisko)
        {
            Studenci.RemoveAll(x => x.Imie == Imie && x.Nazwisko == Nazwisko);
        }
        public int LiczbaStudentow() 
        {
            return Studenci.Count();
        }
        public string PokazGrupe()
        {
            string wynik = "";
            wynik += nazwa + " ";
            wynik += ID + " ";
            return wynik;
        }
        public string PokazStudentowGrupy()
        {
            string wynik = "";
            foreach (Student student in Studenci)
            {
                wynik += student.PokazStudenta()+"\n";
            }
            return wynik;
        }
    }
}
