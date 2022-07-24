using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using automeas_ui.Core;
using System.Threading.Tasks;
using System.IO;

namespace automeas_ui.MWM.Model.Launcher
{
    public class Csv
    {
        public Csv(object master, string path)
        {
            // assuming path doesn't end with \\
            Src = $"{path}\\{DevConfig.Target.DefaultName}.csv";
            // master.NewMeasurement += Event_NewMeasurement;
            Fstream = new FileStream(Src, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            
        }
        ~Csv()
        {
            Fstream.Dispose();
            CsvComplete?.Invoke(Src);
        }
        // events
        public event Action<string>? CsvComplete;
        // handlers
        public void Event_NewMeasurement(string row)
        {
            if (Fstream == null)
            {
                return;
            }
            using StreamWriter sw = File.AppendText(Src);
            sw.WriteLine(row);

        }
        // attr
        private readonly string Src;
        private FileStream Fstream; 
    }
}
