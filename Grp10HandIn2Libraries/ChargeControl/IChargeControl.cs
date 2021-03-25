using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public interface IChargeControl
    {
        bool IsConnected();
        void Charging(object sender, CurrentEventArgs e);
        void StartCharge();
        void StopCharge();
    }
}
