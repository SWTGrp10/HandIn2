using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class RFIDReader : IRFIDReader
    {
        

        public event EventHandler<RFIDEventArgs> RFIDEvent;

        public void ReadRFID(int rfid)
        {
            OnRfidRead(new RFIDEventArgs{RFID = rfid});
        }

        protected virtual void OnRfidRead(RFIDEventArgs e)
        {
            RFIDEvent?.Invoke(this, e);
        }

    }
}
