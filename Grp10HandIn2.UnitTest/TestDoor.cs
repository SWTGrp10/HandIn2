using System;
using System.Collections.Generic;
using System.Text;
using Grp10HandIn2Libraries;
using NSubstitute;
using NUnit.Framework;

namespace Grp10HandIn2.UnitTest
{
    public class TestDoor
    {
        private Door _uut;
        private DoorEventArgs _recievedEventArgs;

        [SetUp]
        public void Setup()
        {
            //var _display = Substitute.For<IDisplay>();
            //var _door = Substitute.For<IDoor>();
            //var _charger = Substitute.For<ICharger>();
            //var _rfid = Substitute.For<IRFIDReader>();
            //_uut = new StationControl(_rfid, _display, _door);
            //_chargeControl = new ChargeControl(_charger);
            _recievedEventArgs = null;

            _uut = new Door();

            _uut.OpenCloseDoor(true);
            _uut.DoorChangedEvent +=
                ((o, args) => { _recievedEventArgs = args; });
        }

        [Test]
        public void Door_DoorSetToNewValue_EventFired()
        {
            //Act
            _uut.OpenCloseDoor(false);

            //Assert
            Assert.That(_recievedEventArgs, Is.Not.Null);
        }

        [Test]
        public void Door_DoorSetToNewValue_CorrectNewValueReceived()
        {
            //Act
            _uut.OpenCloseDoor(false);

            //Assert
            Assert.That(_recievedEventArgs.OpenDoor, Is.EqualTo(false));
        }
    }
}
