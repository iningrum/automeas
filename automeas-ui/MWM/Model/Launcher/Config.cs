using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui.MWM.Model.Launcher
{
    public static class AMDevConfig
    {
        // Liczba stron, nagłówki
        public static List<string> PageTitles = new List<string>
        { // Nagłówki/tytuły View(Modeli) podległych Launcher_MainViewModel
            "Katalog docelowy",
            "Nazwa i opis próby pomiarowej",
            "Konfiguracja próby",
            "Podsumowanie"
        };
        public static int NumberOfPages = PageTitles.Count();        
        // Podpisy przy CheckBoxach
        public static string[] CheckBoxText =
        {
            "*.csv",
            "*.pdf (template required)",
            "*.docx (template required)",
            "*.xslx (template required)",
            "*.jpg (templates required)"
        };
        public static string[] CheckBoxText_Alternative =
        {
            "csv",
            "pdf",
            "docx",
            "xslx",
            "jpg"
        };
        // LauncherSummaryViewModel
        public static List<string> SummaryTitles = new List<string>
        {
            "Nazwa próby:\t\t",
            "Katalog docelowy:\t",
            "Wybrane formaty:\t",
            "Plik cyklu:\t\t",
            "Ilość kroków:\t\t",
            "Szacowany Czas:\t\t"
        };

    }
}
