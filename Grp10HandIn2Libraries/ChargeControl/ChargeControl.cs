﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class ChargeControl : IChargeControl
    {
        ICharger usb;
        IDisplay display = new Display();
        

        public ChargeControl(ICharger usbCharger)
        {
            usb = usbCharger;
            usb.CurrentValueEvent += Charging;
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

            if (usb.Connected)
            {
                if (e.Current != 0)
                {
                    if (e.Current > 0 && e.Current < 5)
                    {
                        display.FullyCharged();
                    }
                    else if (e.Current > 5 && e.Current < 500)
                    {
                        display.OngoingCharge(e.Current);
                    }
                    else if (e.Current > 500)
                    {
                        display.ChargingFail();
                    }
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