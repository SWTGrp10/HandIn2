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
        private IDisplay _display;

        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void ChargeControl_StartCharge_ConnectedIsTrue_startChargeIsCalled()
        {
            //Arrange
            _charger = Substitute.For<ICharger>();
            _uut = new ChargeControl(_charger);
            _charger.Connected = true;

            //Act
            _uut.StartCharge();

            //Assert
            _charger.Received().StartCharge();
        }

        [Test]
        public void ChargeControl_StartCharge_ConnectedIsFalse_startChargeIsCalled()
        {
            //Arrange
            _charger = Substitute.For<ICharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_charger);
            _charger.Connected = false;

            //Act
            _uut.StartCharge();

            //Assert
            _display.Received().ConnectPhone();
        }
    }
}
