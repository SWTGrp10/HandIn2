using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class ChargeControl
    {
        ICharger usb = new USBCharger();
        IDisplay display = new Display();
        private double nowCurrent = new double();

        public ChargeControl(ICharger usbCharger)
        {
            usbCharger.CurrentValueEvent += Charging;
        }

        public ChargeControl()
        {
            
        }

        public bool IsConnected()
        {
            return usb.Connected;

        }

        public void StartCharge()
        {
            if (IsConnected())
            {
                usb.StartCharge();
                
            }
            else
            {
              display.ConnectPhone();
            }
        }


        public void Charging(object sender, CurrentEventArgs e)
        {
            current = e.Current;
            if (usb.Connected)
            {
                if (current != 0)
                {
                    if (current > 0 && e.Current < 5)
                    {
                        display.FullyCharged();
                    }
                    else if (current > 5 && e.Current < 500)
                    {
                        display.OngoingCharge();
                    }
                    else if (current > 500)
                    {
                        display.ChargingFail();
                    }
                }
                
            
        }

        public void StopCharge()
        {
            if (usb.Connected)
            {
                usb.StopCharge();
            }
        }
    }
}
