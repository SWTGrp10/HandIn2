using System;
using System.Collections.Generic;
using System.Text;
using Grp10HandIn2Libraries;
using NUnit.Framework;

namespace Grp10HandIn2.UnitTest
{
    class TestDisplay
    {
        private Display _uut;
        private string testString;
        [SetUp]
        public void Setup()
        {
            _uut = new Display();

        }

        [Test]
        public void Display_ConnectPhone_CorrectStringOutput()
        {
             testString = "Tilslut telefon";
             _uut.ConnectPhone();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_ReadRFID_CorrectStringOutput()
        {
            testString = "Indlæs RFID";
            _uut.ReadRFID();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_ChargingCabinetTaken_CorrectStringOutput()
        {
            testString = "Skabet er låst, og din telefon lades. Brug dit RFID tag til at låse op.";
            _uut.ChargingCabinetTaken();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_ConnectionFail_CorrectStringOutput()
        {
            testString = "Din telefon er ikke ordentlig tilsluttet. Prøv igen.";
            _uut.ConnectionFail();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_RFIDFail_CorrectStringOutput()
        {
            testString = "Forkert RFID tag";
            _uut.RFIDFail();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_RemovePhone_CorrectStringOutput()
        {
            testString = "Tag din telefon ud af skabet og luk døren";
            _uut.RemovePhone();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_FullyCharged_CorrectStringOutput()
        {
            testString = "Telefonen er fuldt opladet";
            _uut.FullyCharged();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_OngoingCharge_CorrectStringOutput()
        {
            testString = "Opladning er i gang";
            _uut.OngoingCharge();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_ChargingFail_CorrectStringOutput()
        {
            testString = "Mulig kortslutning, fjern telefon";
            _uut.ChargingFail();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_NotAvailable_CorrectStringOutput()
        {
            testString = "Ladeskab optaget";
            _uut.NotAvailable();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }

        [Test]
        public void Display_CloseDoor_CorrectStringOutput()
        {
            testString = "Luk venligst døren";
            _uut.CloseDoor();

            Assert.That(_uut.DisplayString, Is.EqualTo(testString));

        }
    }
}
