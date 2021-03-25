using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using Grp10HandIn2Libraries;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace Grp10HandIn2.UnitTest
{
    public class Tests
    {
        private StationControl _uut;
        private ChargeControl _chargeControl;

        private DoorEventArgs _doorEvent;


        [SetUp]
        public void Setup()
        {
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            var _chargeControl = Substitute.For<IChargeControl>();
            _uut = new StationControl(_rfid, _display, _door,_chargeControl);
            _chargeControl = new ChargeControl(_charger);
            _doorEvent = null;
            _door.DoorChangedEvent +=
                ((sender, args) => { _doorEvent = args; });
        }

        [TestCase(true)]
        [TestCase(false)]
        public void StationControl_DoorChanged_CurrentBoolIsCorrect(bool doorChanged)
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _chargeControl = Substitute.For<IChargeControl>();
            _uut = new StationControl(new RFIDReader(), _display, _door, _chargeControl);
            
            //Act
            _door.DoorChangedEvent += Raise.EventWith(new DoorEventArgs {OpenDoor = doorChanged});

            //Assert
            Assert.That(_uut.doorOpen,Is.EqualTo(doorChanged));
        }

        [Test]
        public void StationControl_DoorOpened_CallsConnectPhone()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _chargeControl = Substitute.For<IChargeControl>();
            _uut = new StationControl(new RFIDReader(), _display, _door, _chargeControl);

            //Act
            _door.DoorChangedEvent += Raise.EventWith(new DoorEventArgs { OpenDoor = true });
            _door.DoorChangedEvent += _uut.DoorOpened;

            //Assert
            _display.Received().ConnectPhone();
        }

        [Test]
        public void StationControl_DoorOpened_ChargingCabinetStateDoorOpen()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _chargeControl = Substitute.For<IChargeControl>();
            _uut = new StationControl(new RFIDReader(), _display, _door, _chargeControl);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Available;
            _door.DoorChangedEvent += Raise.EventWith(new DoorEventArgs { OpenDoor = true });
            _door.DoorChangedEvent += _uut.DoorOpened;

            //Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.ChargingCabinetState.DoorOpen));
        }

        [Test]
        public void StationControl_DoorOpened_NotAvailableCalled()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            _uut = new StationControl(new RFIDReader(), _display, _door, _chargeControl);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Locked;
            _door.DoorChangedEvent += Raise.EventWith(new DoorEventArgs { OpenDoor = true });
            _door.DoorChangedEvent += _uut.DoorOpened;

            //Assert
            _display.Received().NotAvailable();
        }


        [Test]
        public void StationControl_DoorClosed_ReadRFID()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            _uut = new StationControl(new RFIDReader(), _display, _door, _chargeControl);

            //Act
            _uut._state = StationControl.ChargingCabinetState.DoorOpen;
            _door.DoorChangedEvent += Raise.EventWith(new DoorEventArgs { OpenDoor = false });
            _door.DoorChangedEvent += _uut.DoorClosed;

            //Assert
            _display.Received().ReadRFID();
        }

        [Test]
        public void StationControl_DoorClosed_ChargingCabinetStateAvailable()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            _uut = new StationControl(new RFIDReader(), _display, _door, _chargeControl);

            //Act
            _uut._state = StationControl.ChargingCabinetState.DoorOpen;
            _door.DoorChangedEvent += Raise.EventWith(new DoorEventArgs { OpenDoor = false });
            _door.DoorChangedEvent += _uut.DoorClosed;

            //Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.ChargingCabinetState.Available));
        }

        [Test]
        public void StationControl_RfidDetectedStateAvailableAndChargerConnected_LockDoor()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            _uut = new StationControl(_rfid, _display, _door, _chargeControl);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Available;
            _charger.Connected = true;
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = 123 });
            _rfid.RFIDEvent += _uut.RfidDetected;

            //Assert
            _door.Received().LockDoor();
        }

        [TestCase(123)]
        [TestCase(456)]
        public void StationControl_RFIDDetected_CurrentIntIsCorrect(int idChanged)
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            _uut = new StationControl(_rfid, _display, _door, _chargeControl);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = idChanged });

            //Assert
            Assert.That(_uut._oldId, Is.EqualTo(idChanged));
        }

        [Test]
        public void StationControl_RfidDetectedStateAvailableAndChargerConnected_ChargingCabinetTaken()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            _uut = new StationControl(_rfid, _display, _door);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Available;
            _charger.Connected = true;
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = 123 });
            _rfid.RFIDEvent += _uut.RfidDetected;

            //Assert
            _display.Received().ChargingCabinetTaken();
        }

        [Test]
        public void StationControl_RfidDetectedStateAvailableAndChargerConnected_ChargingCabinetStateLocked()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            _uut = new StationControl(_rfid, _display, _door);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Available;
            _charger.Connected = true;
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = 123 });
            _rfid.RFIDEvent += _uut.RfidDetected;

            //Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.ChargingCabinetState.Locked));
        }

        //[Test]
        //public void StationControl_RfidDetectedStateAvailableAndChargerNotConnected_ConnectionFailCalled()
        //{
        //    //Arrange
        //    var _display = Substitute.For<IDisplay>();
        //    var _door = Substitute.For<IDoor>();
        //    var _charger = Substitute.For<ICharger>();
        //    var _rfid = Substitute.For<IRFIDReader>();
        //    _uut = new StationControl(_rfid, _display, _door);
        //    _chargeControl = new ChargeControl(_charger);

        //    //Act


        //    _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = 123 });
        //    _uut._state = StationControl.ChargingCabinetState.Available;
        //    _charger.Connected = false;
        //    _rfid.RFIDEvent += _uut.RfidDetected;

        //    //Assert
        //    _display.Received().ConnectionFail();
        //}
        [Test]
        public void StationControl_RfidDetectedStateDoorOpen_CloseDoor()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            _uut = new StationControl(_rfid, _display, _door);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _uut._state = StationControl.ChargingCabinetState.DoorOpen;
            _charger.Connected = true;
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = 123 });
            _rfid.RFIDEvent += _uut.RfidDetected;

            //Assert
            _display.Received().CloseDoor();
        }

        [Test]
        public void StationControl_RfidDetectedStateLockedAndOldidEqualsRFID_UnLockDoor()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            _uut = new StationControl(_rfid, _display, _door);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Locked;
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = _uut._oldId });
            _rfid.RFIDEvent += _uut.RfidDetected;

            //Assert
            _door.Received().UnlockDoor();
        }

        [Test]
        public void StationControl_RfidDetectedStateLockedAndOldidEqualsRFID_RemovePhone()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            _uut = new StationControl(_rfid, _display, _door);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Locked;
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = _uut._oldId });
            _rfid.RFIDEvent += _uut.RfidDetected;

            //Assert
            _display.Received().RemovePhone();
        }

        [Test]
        public void StationControl_RfidDetectedStateLockedAndOldidEqualsRFID_ChargingCabinetStateLocked()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            _uut = new StationControl(_rfid, _display, _door);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Locked;
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = _uut._oldId });
            _rfid.RFIDEvent += _uut.RfidDetected;

            //Assert
            Assert.That(_uut._state, Is.EqualTo(StationControl.ChargingCabinetState.Available));
        }

        [Test]
        public void StationControl_RfidDetectedStateLockedAndOldidNotEqualRFID_RFIDFail()
        {
            //Arrange
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            var _charger = Substitute.For<ICharger>();
            var _rfid = Substitute.For<IRFIDReader>();
            _uut = new StationControl(_rfid, _display, _door);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Locked;
            _uut._oldId = 123;
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = 456 });
            _rfid.RFIDEvent += _uut.RfidDetected;

            //Assert
            _display.Received().RFIDFail();
        }
    }

}