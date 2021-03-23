using System;
using System.Runtime.CompilerServices;
using Grp10HandIn2Libraries;


namespace Grp10HandIn2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.Write("Test");
            var door = new Door();
            var rfidReader = new RFIDReader(); 
            IDisplay display = new Display();
            var stationController = new StationControl(rfidReader, display, door);

            // Assemble your system here from all the classes

            bool finish = false;
            
            do
            {
                string input;
                System.Console.WriteLine("Indtast E for exit, O for open, C for close, R for rfid read: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OpenCloseDoor(true);
                        break;

                    case 'C':
                        door.OpenCloseDoor(false);
                        break;

                    case 'R':
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.ReadRFID(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
