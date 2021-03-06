using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public interface IDisplay
    {
        void ConnectPhone();
        void ReadRFID();
        void ChargingCabinetTaken();
        void ConnectionFail();
        void RFIDFail();
        void RemovePhone();
        void FullyCharged();
        void OngoingCharge(double current);
        void ChargingFail();
        void NotAvailable();
        void CloseDoor();
    }
}
