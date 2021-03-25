using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_charger, _display);
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
            _uut = new ChargeControl(_charger, _display);
            _charger.Connected = false;

            //Act
            _uut.StartCharge();

            //Assert
            _display.Received().ConnectPhone();
        }

        [Test]
        public void ChargeControl_StopCharge_ConnectedIsTrue_stopChargeIsCalled()
        {
            //Arrange
            _charger = Substitute.For<ICharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_charger, _display);
            _charger.Connected = true;

            //Act
            _uut.StopCharge();

            //Assert
            _charger.Received().StopCharge();
        }

        [TestCase(false,false)]
        [TestCase(true,true)]
        public void ChargeControl_IsConnected_ReturnBoolIsConnected(bool charger_Connected, bool result)
        {
            //Arrange
            _charger = Substitute.For<ICharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_charger,_display);

            //Act
            _charger.Connected = charger_Connected;


            //Assert
            Assert.That(_uut.IsConnected, Is.EqualTo(result));
        }

        [Test]
        public void ChargeControl_Charging_CurrentIs4_DisplayRecievesFullyCharged()
        {
            //Arrange
            _charger = Substitute.For<ICharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_charger,_display);
            _charger.Connected = true;

            //Act
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = 4.7});
            _charger.CurrentValueEvent += _uut.Charging;

            //Assert
            _display.Received(1).FullyCharged();
        }


        [TestCase(499.7)]
        [TestCase(5.5)]
        public void ChargeControl_Charging_CurrentIs300_DisplayRecievesOnGoingCharge(double current)
        {
            //Arrange
            _charger = Substitute.For<ICharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_charger, _display);

            _charger.Connected = true;


            //Act
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            _charger.CurrentValueEvent += _uut.Charging;

            
            //Assert
            _display.Received().OngoingCharge(current);
        }

        [TestCase(500.5)]
        public void ChargeControl_Charging_CurrentOver500_DisplayRecievesChargingFail(double current)
        {
            //Arrange
            _charger = Substitute.For<ICharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_charger, _display);

            _charger.Connected = true;


            //Act
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            _charger.CurrentValueEvent += _uut.Charging;


            //Assert
            _display.Received().ChargingFail();
        }

    }
}
