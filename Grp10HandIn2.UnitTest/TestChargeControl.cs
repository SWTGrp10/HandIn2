using System;
using System.Collections.Generic;
using System.Text;
using Grp10HandIn2Libraries;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
namespace Grp10HandIn2.UnitTest

{
    class TestChargeControl
    {
        private IChargeControl _uut;
        private ICharger _charger;

        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void ChargeControl_StartCharge_usb_startChargeIsCalled()
        {
            //Arrange
            _charger = Substitute.For<ICharger>();
            _uut = new ChargeControl(_charger);


            //Act
            _charger.CurrentValueEvent += Raise.Event(new CurrentEventArgs{})
            += Raise.EventWith(new DoorEventArgs { OpenDoor = false });
            _door.DoorChangedEvent += _uut.DoorClosed;

            //Assert
            _display.Received().ReadRFID();
        }
}
