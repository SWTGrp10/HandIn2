using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class CurrentEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Current { set; get; }
    }

    interface ICharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;

        // Direct access to the current current value
        double CurrentValue { get; }

        // Require connection status of the phone
        bool Connected { get; }

        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
    }
}
