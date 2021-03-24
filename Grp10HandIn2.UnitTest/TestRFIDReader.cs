using System;
using System.Collections.Generic;
using System.Text;
using Grp10HandIn2Libraries;
using NUnit.Framework;

namespace Grp10HandIn2.UnitTest
{
    public class TestRFIDReader
    {
        private RFIDReader _uut;
        private RFIDEventArgs _recievedEventArgs;

        [SetUp]
        public void Setup()
        {
            _recievedEventArgs = null;

            _uut = new RFIDReader();

            _uut.ReadRFID(123);
            _uut.RFIDEvent +=
                ((o, args) => { _recievedEventArgs = args; });
        }

        [Test]
        public void Door_DoorSetToNewValue_EventFired()
        {
            //Act
            _uut.ReadRFID(456);

            //Assert
            Assert.That(_recievedEventArgs, Is.Not.Null);
        }

        [Test]
        public void Door_DoorSetToNewValue_CorrectNewValueReceived()
        {
            //Act
            _uut.ReadRFID(456);

            //Assert
            Assert.That(_recievedEventArgs.RFID, Is.EqualTo(456));
        }
    }
}
