using System;
using System.Collections.Generic;
using System.Text;
using Grp10HandIn2Libraries;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using System.Threading;

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

        [Test]
        public void ChargeControl_StopCharge_ConnectedIsTrue_stopChargeIsCalled()
        {
            //Arrange
            _charger = Substitute.For<ICharger>();
            _uut = new ChargeControl(_charger);
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
            _uut = new ChargeControl(_charger);

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
            _uut = new ChargeControl(_charger);
            _charger.Connected = true;

            //Act
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = 4.7});
            _charger.CurrentValueEvent += _uut.Charging;

            //Assert
            _display.Received().FullyCharged();
        }


        [Test]
        public void ChargeControl_Charging_CurrentIs300_DisplayRecievesOnGoingCharge()
        {
            //Arrange
            _charger = Substitute.For<ICharger>();

            _charger.Connected = true;
            _display = Substitute.For<IDisplay>();

            _uut = new ChargeControl(_charger);
            System.Threading.Thread.Sleep(1100);

            //Act
            _charger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs());
            _charger.CurrentValueEvent += _uut.Charging;

            

            //Assert
            _display.Received().OngoingCharge(302.1);
        }


    }
}
