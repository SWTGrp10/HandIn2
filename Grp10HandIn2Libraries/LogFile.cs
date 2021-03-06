using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class LogFile : ILogFile
    {
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        public void WriteToLogLocked(int id)
        {

            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
            }
        }

        public void WriteToLogUnlocked(int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
            }
        }
    }
}
