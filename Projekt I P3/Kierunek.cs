using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_I_P3
{

    internal class Kierunek
    {
        public int ID;
        private static int LiczbaKierunkow;

        public string nazwa;

        public int LiczbaSemestrow;

        public List<Grupa> Grupy = new List<Grupa>();
        public List<Przedmiot> Przedmioty = new List<Przedmiot>();

        public Kierunek(string nazwa,int LiczbaSem) 
        { 
            this.LiczbaSemestrow = LiczbaSem;
            this.nazwa = nazwa;
            ID = LiczbaKierunkow; LiczbaKierunkow++;
            Przedmioty.Add(new Przedmiot("WF"));
            Przedmioty.Add(new Przedmiot("Angielski"));
        }
        public void DodajGrupe(string Nazwa) 
        {
            Grupy.Add(new Grupa(Nazwa,LiczbaSemestrow)); 
        }
        public void UsunGrupe(int id) 
        {
            Grupy.RemoveAt(id);
        }
        public void UsunGrupe(string Nazwa) 
        {
            Grupy.RemoveAll(x => x.nazwa == Nazwa);
        }
        public void UsunPrzedmiot(int id) 
        {
            Przedmioty.RemoveAt(id);
        }
        public void UsunPrzedmiot(string Nazwa)
        {
            Przedmioty.RemoveAll(x => x.Nazwa == Nazwa);
        }
        public void DodajPrzedmiot(string Nazwa) 
        {
            Przedmioty.Add(new Przedmiot(Nazwa));
        }
        public string PokazKierunek()
        {
            string wynik = "";
            wynik += nazwa+" ";
            wynik += "Semestry: "+LiczbaSemestrow+" ";
            //wynik += ID+" ";
            return wynik;
        }
        public string PokazStudentowKierunku()
        {
            string wynik="";
            foreach (Grupa grupa in Grupy)
            {
                wynik += grupa.PokazStudentowGrupy()+"\n";
            }
            return wynik;
        }
    }
}
