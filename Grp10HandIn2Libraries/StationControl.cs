using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Grp10HandIn2Libraries
{
    public class StationControl : StationControlSubject
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum ChargingCabinetState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private ChargingCabinetState _state;
        private ICharger _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _display;
        private ILogFile _logfile;

        //private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        public StationControl()
        {
            _charger = new USBCharger();
            _door = new Door();
            _display = new Display();
            _logfile = new LogFile();
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case ChargingCabinetState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;

                        _logfile.WriteToLogLocked(id);
                        
                        _display.ChargingCabinetTaken();
                        _state = ChargingCabinetState.Locked;
                    }
                    else
                    {
                        _display.ConnectionFail();
                    }

                    break;

                case ChargingCabinetState.DoorOpen:
                    // Ignore
                    break;

                case ChargingCabinetState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        
                        _logfile.WriteToLogUnlocked(id);

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
        public void DoorOpened()
        {
            //Notify();
            _display.ConnectPhone();
            _state = ChargingCabinetState.DoorOpen;
        }

        public void DoorClosed()
        {
            //Notify();
            _display.ReadRFID();
            _state = ChargingCabinetState.Available;
        }
    }
}
