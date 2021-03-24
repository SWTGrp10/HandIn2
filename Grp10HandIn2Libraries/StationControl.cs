using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum ChargingCabinetState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        public ChargingCabinetState _state;
        private ChargeControl _chargeControl;
        public int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private ILogFile _logfile;
        public bool doorOpen { get; set; }

        //private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        
        public StationControl(IRFIDReader rfidReader, IDisplay display, IDoor door)
        {
            door.DoorChangedEvent += DoorOpened;
            door.DoorChangedEvent += DoorClosed;
            rfidReader.RFIDEvent += RfidDetected;
            _chargeControl = new ChargeControl();
            _door = door;
            _display = display;
            _logfile = new LogFile();
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        public void RfidDetected(object sender, RFIDEventArgs e)
        {
            switch (_state)
            {
                case ChargingCabinetState.Available:
                    // Check for ladeforbindelse
                    if (_chargeControl.IsConnected())
                    {
                        _door.LockDoor();
                        _chargeControl.StartCharge(); //Ikke testet
                        _oldId = e.RFID;

                        _logfile.WriteToLogLocked(_oldId); //ikke testet
                        
                        _display.ChargingCabinetTaken();
                        _state = ChargingCabinetState.Locked;
                        
                    }
                    else
                    {
                        _display.ConnectionFail(); //Test virker ikke
                    }

                    break;

                case ChargingCabinetState.DoorOpen:
                {
                    _display.CloseDoor();
                }
                    break;

                case ChargingCabinetState.Locked:
                    // Check for correct ID
                    if (e.RFID == _oldId)
                    {
                        _chargeControl.StopCharge(); //Ikke testet
                        _door.UnlockDoor();
                        
                        _logfile.WriteToLogUnlocked(_oldId); //ikke testet

                        _display.RemovePhone();
                        _state = ChargingCabinetState.Available;
                    }
                    else
                    {
                        _display.RFIDFail();
                    }

                    break;
            }
        }

        // Her mangler de andre trigger handlere
        public void DoorOpened(object sender, DoorEventArgs e)
        {
            doorOpen = e.OpenDoor;
            if (doorOpen)
            {
                if (_state == ChargingCabinetState.Available)
                {
                    _state = ChargingCabinetState.DoorOpen;
                    Console.WriteLine("Døren er åbnet");
                    _display.ConnectPhone();
                }
                else
                {
                    _display.NotAvailable();
                }
            }
        }

        public void DoorClosed(object sender, DoorEventArgs e)
        {
            doorOpen = e.OpenDoor;
            if (!doorOpen)
            {
                if (_state == ChargingCabinetState.DoorOpen)
                {
                    _state = ChargingCabinetState.Available;
                    Console.WriteLine("Døren er lukket");
                    _display.ReadRFID();
                }
                else
                {
                    Console.WriteLine("Døren kan ikke lukkes igen");
                }
            }
        }
    }
}
