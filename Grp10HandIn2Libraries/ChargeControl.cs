using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class ChargeControl
    {
        ICharger usb;
        IDisplay display = new Display();
        private double nowCurrent = new double();

        public ChargeControl(ICharger usbCharger)
        {
            usb = usbCharger;
            usb.CurrentValueEvent += Charging;
        }

        public bool IsConnected()
        {
            return usb.Connected;

        }

        public void StartCharge()
        {
            if (usb.Connected)
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
            nowCurrent = e.Current;  
                if (nowCurrent != 0)
                {
                    if (nowCurrent > 0 && nowCurrent < 5)
                    {
                        display.FullyCharged();
                    }
                    else if (nowCurrent > 5 && nowCurrent < 500)
                    {
                        display.OngoingCharge();
                    }
                    else if (nowCurrent > 500)
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
