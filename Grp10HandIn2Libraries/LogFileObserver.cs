using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Grp10HandIn2Libraries
{
    class LogFileObserver : IObserver
    {
        private StationControl stationControl = new StationControl();
        private string logFile = "logfile.txt"; // Navnet på systemets log-fil
        public void Update()
        {
          
        }
    }
}
