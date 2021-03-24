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
            _uut = new StationControl(_rfid, _display, _door);
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
            _uut = new StationControl(new RFIDReader(), _display, _door);
            
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
            _uut = new StationControl(new RFIDReader(), _display, _door);

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
            _uut = new StationControl(new RFIDReader(), _display, _door);

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
            _uut = new StationControl(new RFIDReader(), _display, _door);

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
            _uut = new StationControl(new RFIDReader(), _display, _door);

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
            _uut = new StationControl(new RFIDReader(), _display, _door);

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
            _uut = new StationControl(_rfid, _display, _door);
            _chargeControl = new ChargeControl(_charger);

            //Act
            _uut._state = StationControl.ChargingCabinetState.Available;
            _charger.Connected = true;
            _rfid.RFIDEvent += Raise.EventWith(new RFIDEventArgs { RFID = 123 });
            _rfid.RFIDEvent += _uut.RfidDetected;

            //Assert
            _door.Received().LockDoor();
        }

        //}

        //internal class FakeDoor : IDoor
        //{
        //    public int checkLock = 0;

        //    public event EventHandler<DoorEventArgs> DoorEvent;

        //    public void LockDoor()
        //    {
        //        checkLock = 1;
        //    }

        //    public void UnlockDoor()
        //    {
        //        checkLock = 2;
        //    }

        //    public void OnDoorOpen()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void OnDoorClose()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //internal class FakeCharger : ICharger
        //{
        //    public event EventHandler<CurrentEventArgs> CurrentValueEvent;
        //    public double CurrentValue { get; }
        //    public bool Connected { get; set; }
        //    public void StartCharge()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void StopCharge()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //internal class FakeDisplay : IDisplay
        //{
        //    public int check = 0;
        //    public void ConnectPhone()
        //    {
        //        check = 1;
        //    }

        //    public void ReadRFID()
        //    {
        //        check = 2;
        //    }

        //    public void ChargingCabinetTaken()
        //    {
        //        check = 3;
        //    }

        //    public void ConnectionFail()
        //    {
        //        check = 4;
        //    }

        //    public void RFIDFail()
        //    {
        //        check = 5;
        //    }

        //    public void RemovePhone()
        //    {
        //        check = 6;
        //    }
    }

}