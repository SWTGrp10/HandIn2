using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public interface IRFIDReader
    {
        event EventHandler<RFIDEventArgs> RFIDEvent;
    }
}
