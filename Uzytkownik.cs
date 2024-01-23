using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1
{
    public class Uzytkownik
    {
        public Uzytkownik() { }
        public Uzytkownik(string Nazwa, DateTime DataUruchomienia) { this.Nazwa = Nazwa; this.DataUruchomienia = DataUruchomienia; }
        public string Nazwa { get; set; }
        public DateTime DataUruchomienia { get; set; }
        public string DataUruchomieniaString { get; set; }
        public string GodzinaUruchomienia { get; set; }
        public int LiczbaZmianRysunku { get; set; }
        public int LiczbaZmianPolozeniaRysunku { get; set; }
        public int LiczbaZmianRozmiaru { get; set; }
        public TimeSpan CzasMaksymanlny { get; set; }
        public TimeSpan CzasMinimalny { get; set; }
    }
}