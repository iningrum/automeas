using System.Collections.Generic;
using System.Linq;

namespace automeas_ui.MWM.Model
{
    public static class PageTitles
    {
        static public string Get(int id)
        {
            List<string> results = new List<string>{
                "Katalog docelowy",
                "Nazwa i opis próby pomiarowej",
                "Nazwa i opis próby pomiarowej",
                "Konfiguracja próby",
                "Podsumowanie"
            };
            return results.ElementAt(id);
        }

    }
}
