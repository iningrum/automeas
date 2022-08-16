﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace automeas_ui._Common
{
    /// <summary>
    /// Class <c> DevConfig </c> is used for internal configuration of databound xaml elements.
    /// </summary>
    public partial class DevConfig
    { // Launcher
        /// <summary>
        /// Titles of pages displayed by <c> Launcher </c>
        /// </summary>
        public static List<string> PageTitles = new List<string>
        { // Nagłówki/tytuły View(Modeli) podległych Launcher_MainViewModel
            "Katalog docelowy",
            "Nazwa i opis próby pomiarowej",
            "Konfiguracja próby",
            "Podsumowanie"
        };
        /// <summary>
        /// An alias for <c>PageTitles.Count()</c>
        /// </summary>
        public static int NumberOfPages = PageTitles.Count();
        /// <summary>
        /// Checkbox titles for <c>Page1</c> of <c>Launcher</c>
        /// </summary>
        public static string[] CheckBoxText =
        {
            "*.csv",
            "*.pdf (template required)",
            "*.docx (template required)",
            "*.xslx (template required)",
            "*.jpg (templates required)"
        };
        /// <summary>
        /// Alternative checkbox titles for <c>Page4</c> of <c>Launcher</c>
        /// </summary>
        public static string[] CheckBoxText_Alternative =
        {
            "csv",
            "pdf",
            "docx",
            "xslx",
            "jpg"
        };
        /// <summary>
        /// Labels used for <c>Page4</c> of <c>Launcher</c>
        /// </summary>
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