using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    class ChargeControl
    {
        ICharger usb = new USBCharger();
        IDisplay display = new Display();
        public bool IsConnected()
        {
            return usb.Connected;

        }

        public void StartCharge()
        {
            if(usb.Connected)
            usb.StartCharge();
            else
            {
              display.ConnectPhone();
            }
        }

        public void StopCharge()
        {
            if (IsConnected())
            {
                usb.StopCharge();
            }
        }
    }
}
