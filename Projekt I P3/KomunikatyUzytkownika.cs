using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_I_P3
{
    internal class KomunikatyUzytkownika
    {
        public void Error_NazwaJuzZajeta()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("OBIEKT O PODANEJ NAZWIE JUŻ ISTNIEJE");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
