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
