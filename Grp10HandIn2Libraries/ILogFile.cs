using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    interface ILogFile
    {
        void WriteToLogLocked(int id);
        void WriteToLogUnlocked(int id);
    }
}
