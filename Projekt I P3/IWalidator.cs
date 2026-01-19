namespace Projekt_I_P3
{
    internal interface IWalidator
    {
        string Wprowadz_Wydzial();
        string Wprowadz_Kierunek(string wydzial);
        string Wprowadz_Grupe(string wydzial, string kierunek);
        string Wprowadz_Przedmiot(string wydzial, string kierunek);
        bool Istnieje_Wydzial(string wydzial);
        bool Istnieje_Kierunek(string wydzial, string kierunek);
        bool Istnieje_Grupa(string wydzial, string kierunek,string grupa);
        bool Istnieje_Przedmiot(string wydzial, string kierunek,string przedmiot);
        bool SprawdzEmail(string email);
    }
}