using System;
using System.Collections.Generic;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class Display : IDisplay
    {
        public void ConnectPhone()
        {
            Console.WriteLine("Tilslut telefon");
        }

        public void ReadRFID()
        {
            Console.WriteLine("Indlæs RFID");
        }

        public void ChargingCabinetTaken()
        {
            Console.WriteLine("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        public void ConnectionFail()
        {
            Console.WriteLine("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        public void RFIDFail()
        {
            Console.WriteLine("Forkert RFID tag");
        }

        public void RemovePhone()
        {
            Console.WriteLine("Tag din telefon ud af skabet og luk døren");
        }

        public void FullyCharged()
        {
            Console.WriteLine("Telefonen er fuldt opladet");
        }

        public void UngoingCharge()
        {
            Console.WriteLine("Opladning er i gang");
        }

        public void ChargingFail()
        {
            Console.WriteLine("Mulig kortslutning, fjern telefon");
        }

        //Har implementeret disse metoder, da de var i interface og viste fejl. Kh Emma
        public void NotAvailable()
        {
            throw new NotImplementedException();
        }

        public void CloseDoor()
        {
            throw new NotImplementedException();
        }
    }
}
