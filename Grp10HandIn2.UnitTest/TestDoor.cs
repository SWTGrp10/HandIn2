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
        private string testString;

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

        [Test]
        public void Door_LockDoor_CorrectStringOutput()
        {
            testString = "Door has been locked";
            _uut.LockDoor();

            Assert.That(_uut.DoorString, Is.EqualTo(testString));

        }

        [Test]
        public void Door_UnLockDoor_CorrectStringOutput()
        {
            testString = "Door has been unlocked";
            _uut.UnlockDoor();

            Assert.That(_uut.DoorString, Is.EqualTo(testString));

        }
    }
}
