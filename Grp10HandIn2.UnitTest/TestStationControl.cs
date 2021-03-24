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

        private DoorEventArgs _doorEvent;

        //private FakeDisplay _display;
        //private FakeCharger _charger;
        //private FakeDoor _door;

        [SetUp]
        public void Setup()
        {
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            _uut = new StationControl(new RFIDReader(), _display, _door);
            _doorEvent = null;
            _door.DoorChangedEvent +=
                ((sender, args) => { _doorEvent = args; });
        }

        [TestCase(true)]
        [TestCase(false)]
        public void StationControl_DoorChanged_CurrentBoolIsCorrect(bool doorChanged)
        {
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            _uut = new StationControl(new RFIDReader(), _display, _door);
            _door.DoorChangedEvent += Raise.EventWith(new DoorEventArgs {OpenDoor = doorChanged});
            Assert.That(_uut.doorOpen,Is.EqualTo(doorChanged));
        }

        [Test]
        public void StationControl_DoorOpened_CallsConnectPhone()
        {
            var _display = Substitute.For<IDisplay>();
            var _door = Substitute.For<IDoor>();
            _uut = new StationControl(new RFIDReader(), _display, _door);
            _doorEvent = null;
            _door.DoorChangedEvent +=
                ((sender, args) => { _doorEvent = args; });

            _uut.DoorOpened(_door, _doorEvent);
            _display.Received().ConnectPhone();
        }

        //    [Test]
        //    public void StationControl_DoorOpened_ChargingCabinetStateDoorOpen()
        //    {
        //        _uut.DoorOpened();
        //        Assert.That(_uut._state, Is.EqualTo(StationControl.ChargingCabinetState.DoorOpen));
        //    }

        //    [Test]
        //    public void StationControl_DoorClosed_ReadRFID()
        //    {
        //        _uut.DoorClosed();
        //        Assert.That(_display.check, Is.EqualTo(2));
        //    }

        //    [Test]
        //    public void StationControl_DoorClosed_ChargingCabinetStateAvailable()
        //    {
        //        _uut.DoorClosed();
        //        Assert.That(_uut._state, Is.EqualTo(StationControl.ChargingCabinetState.Available));
        //    }

        //    [Test]
        //    public void StationControl_RfidDetectedStateAvailableAndChargerConnected_LockDoor()
        //    {
        //        _uut._state = StationControl.ChargingCabinetState.Available;
        //        _uut._charger.Connected = true;
        //        _uut.RfidDetected(new object(), new RFIDEventArgs());
        //        Assert.That(_door.checkLock, Is.EqualTo(1));
        //    }

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