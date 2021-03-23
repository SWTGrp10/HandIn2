using Grp10HandIn2Libraries;
using NUnit.Framework;

namespace Grp10HandIn2.UnitTest
{
    public class Tests
    {
        private StationControl _uut;
        private FakeDisplay _display;

        [SetUp]
        public void Setup()
        {
            _display = new FakeDisplay();
            _uut = new StationControl(new RFIDReader(), _display);
            
        }

        [Test]
        public void StationControl_DoorOpened_CallsConnectPhone()
        {
            _uut.DoorOpened();
            Assert.That(_display.check, Is.EqualTo(1));
        }

        [Test]
        public void StationControl_DoorOpened_ChargingCabinetStateDoorOpen()
        {
            _uut.DoorOpened();
            Assert.That(_uut._state, Is.EqualTo(StationControl.ChargingCabinetState.DoorOpen));
        }

        [Test]
        public void StationControl_DoorClosed_ReadRFID()
        {
            _uut.DoorClosed();
            Assert.That(_display.check, Is.EqualTo(2));
        }

        [Test]
        public void StationControl_DoorClosed_ChargingCabinetStateAvailable()
        {
            _uut.DoorClosed();
            Assert.That(_uut._state, Is.EqualTo(StationControl.ChargingCabinetState.Available));
        }

    }

    internal class FakeDisplay : IDisplay
    {
        public int check = 0;
        public void ConnectPhone()
        {
            check = 1;
        }

        public void ReadRFID()
        {
            check = 2;
        }

        public void ChargingCabinetTaken()
        {
            check = 3;
        }

        public void ConnectionFail()
        {
            check = 4;
        }

        public void RFIDFail()
        {
            check = 5;
        }

        public void RemovePhone()
        {
            check = 6;
        }
    }
}