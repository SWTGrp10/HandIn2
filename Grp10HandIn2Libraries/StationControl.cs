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
        public ICharger _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private ILogFile _logfile;

        //private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        
        public StationControl(IRFIDReader rfidReader, IDisplay display, IDoor door)
        {
            door.DoorEvent += DoorOpened;
            door.DoorEvent += DoorClosed;
            rfidReader.RFIDEvent += RfidDetected;
            _charger = new USBCharger();
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
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = e.RFID;

                        _logfile.WriteToLogLocked(_oldId);
                        
                        _display.ChargingCabinetTaken();
                        _state = ChargingCabinetState.Locked;
                    }
                    else
                    {
                        _display.ConnectionFail();
                    }

                    break;

                case ChargingCabinetState.DoorOpen:
                   //ignore
                    break;

                case ChargingCabinetState.Locked:
                    // Check for correct ID
                    if (e.RFID == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        
                        _logfile.WriteToLogUnlocked(_oldId);

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
            _display.ConnectPhone();

                _state = ChargingCabinetState.DoorOpen;

        }

        public void DoorClosed(object sender, DoorEventArgs e)
        {
            _display.ReadRFID();
            _state = ChargingCabinetState.Available;
        }
    }
}
