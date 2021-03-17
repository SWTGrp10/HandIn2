using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    interface IDisplay
    {
        void ConnectPhone();
        void ReadRFID();
        void ChargingCabinetTaken();
        void RFIDFail();
        void RemovePhone();
    }
}
