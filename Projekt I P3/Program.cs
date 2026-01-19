using System.IO;
using Newtonsoft.Json;

namespace Projekt_I_P3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ZarzadzanieUczelnia Zuczelnia;

            if (File.Exists("db.json"))
            {
                string json = File.ReadAllText("db.json");
                Zuczelnia = JsonConvert.DeserializeObject<ZarzadzanieUczelnia>(json)
                           ?? new ZarzadzanieUczelnia();
                // operator ?? sprawdza czy jest tam null
            }
            else
            {
                Zuczelnia = new ZarzadzanieUczelnia();
            }
            Interfejs interfejs = new Interfejs(Zuczelnia,Zuczelnia.walidator,new KomunikatyUzytkownika());
            do{
                string outputJson = JsonConvert.SerializeObject(Zuczelnia, Formatting.Indented);
                File.WriteAllText("db.json", outputJson);
            } while (interfejs.Update());  
        }
    }
}

/*
1. Dodac sprawdzanie czy to co uzytkownik wprowadza jest w poprawnym formacie
2. Obrazy?
3. LINQ wyszukiwanie (Ile było czegoś), wykres zrobić

 */