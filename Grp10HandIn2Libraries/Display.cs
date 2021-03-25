using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class Display : IDisplay
    {
        public string DisplayString { get; private set; }
        public void ConnectPhone()
        {
            DisplayString = "Tilslut telefon";
            Console.WriteLine(DisplayString);
        }

        public void ReadRFID()
        {
            DisplayString = "Indlæs RFID";
            Console.WriteLine(DisplayString);
        }

        public void ChargingCabinetTaken()
        {
            DisplayString = "Skabet er låst, og din telefon lades. Brug dit RFID tag til at låse op.";
            Console.WriteLine(DisplayString);
        }

        public void ConnectionFail()
        {
            DisplayString = "Din telefon er ikke ordentlig tilsluttet. Prøv igen.";
            Console.WriteLine(DisplayString);
        }

        public void RFIDFail()
        {
            DisplayString = "Forkert RFID tag";
            Console.WriteLine(DisplayString);
        }

        public void RemovePhone()
        {
            DisplayString="Tag din telefon ud af skabet og luk døren";
            Console.WriteLine(DisplayString);
        }

        public void FullyCharged()
        {
            DisplayString = "Telefonen er fuldt opladet";
            Console.WriteLine(DisplayString);
        }

        public void OngoingCharge()
        {
            DisplayString = "Opladning er i gang";
            Console.WriteLine(DisplayString);
        }

        public void ChargingFail()
        {
            DisplayString = "Mulig kortslutning, fjern telefon";
            Console.WriteLine(DisplayString);
        }

        public void NotAvailable()
        {
            DisplayString = "Ladeskab optaget";
            Console.WriteLine(DisplayString);
        }

        public void CloseDoor()
        {
            DisplayString = "Luk venligst døren";
            Console.WriteLine(DisplayString);
        }
    }
}
