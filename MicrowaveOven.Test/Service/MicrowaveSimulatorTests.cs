using Microwave.Service;
using MicrowaveOven.Service;
using MicrowaveOven.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.Test.Service
{
    [TestClass]
    public class MicrowaveSimulatorTests
    {
        private MicrowaveOvenSimulator _microwaveSimulator;
        private IMicrowaveOvenHW _hardware;

        [TestInitialize]
        public void Setup()
        {
            _hardware = new MicrowaveSimulator();
            _microwaveSimulator = new MicrowaveOvenSimulator(_hardware);
        }

        [TestMethod]
        public void SetDoorOpen_WhenCalled_UpdatesDoorOpenState()
        {
            // Arrange
            bool expected = true;

            // Act
            _microwaveSimulator.SetDoorOpen(expected);

            // Assert
            Assert.IsTrue(_microwaveSimulator.DoorOpen);
        }

        [TestMethod]
        public void SetDoorOpen_RaisesDoorOpenChangedEvent()
        {
            // Arrange
            bool eventFired = false;
            bool eventValue = false;

            _microwaveSimulator.DoorOpenChanged += (isOpen) =>
            {
                eventFired = true;
                eventValue = isOpen;
            };

            // Act
            _microwaveSimulator.SetDoorOpen(true);

            // Assert
            Assert.IsTrue(eventFired, "DoorOpenChanged event was not fired.");
            Assert.IsTrue(eventValue, "DoorOpenChanged event did not return correct value.");
        }
        [TestMethod]
        public void SimulateStartButtonPress_True_SetsStartButtonValueAndRaisesEvent()
        {
            // Arrange
            bool eventRaised = false;

            _microwaveSimulator.StartButtonPressed += (sender, args) =>
            {
                eventRaised = true;
            };

            // Act
            _microwaveSimulator.SimulateStartButtonPress(true);

            // Assert
            Assert.IsTrue(_microwaveSimulator.StartButtonValue);
            Assert.IsTrue(eventRaised, "StartButtonPressed event was not raised.");
        }

        [TestMethod]
        public void SimulateStartButtonPress_False_SetsStartButtonValueAndDoesNotRaiseEvent()
        {
            // Arrange
            bool eventRaised = false;

            _microwaveSimulator.StartButtonPressed += (sender, args) =>
            {
                eventRaised = true;
            };

            // Act
            _microwaveSimulator.SimulateStartButtonPress(false);

            // Assert
            Assert.IsFalse(_microwaveSimulator.StartButtonValue);
            Assert.IsFalse(eventRaised, "StartButtonPressed event should not be raised for false.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TurnOnHeater_SetsDoorOpenToFalse()
        {
            // Arrange
            _microwaveSimulator.SetDoorOpen(true);
            Assert.IsTrue(_microwaveSimulator.DoorOpen); // Precondition

            // Act
            _microwaveSimulator.TurnOnHeater();

            
        }

        [TestMethod]
        public void TurnOffHeater_DoesNotChangeDoorOpenState()
        {
            // Arrange
            _microwaveSimulator.SetDoorOpen(true);

            // Act
            _microwaveSimulator.TurnOffHeater();

            // Assert
            Assert.IsTrue(_microwaveSimulator.DoorOpen);
        }

    }
}
