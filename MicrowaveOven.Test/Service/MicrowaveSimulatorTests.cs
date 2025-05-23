﻿using MicrowaveOven.Hardware.Service;
using MicrowaveOven.Service.Impl;
using Moq;

namespace MicrowaveOven.Test.Service
{
    [TestClass]
    public class MicrowaveSimulatorTests
    {
        private MicrowaveOvenSimulator _microwaveSimulator;
        private Mock<IMicrowaveOvenHW> _mockHardware;

        [TestInitialize]
        public void Setup()
        {
            _mockHardware = new Mock<IMicrowaveOvenHW>();
            _microwaveSimulator = new MicrowaveOvenSimulator(_mockHardware.Object);
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
        public void SimulateStartButtonPress_False_SetsStartButtonValueAndRaiseEventForStopButton()
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
            Assert.IsTrue(eventRaised, "StartButtonPressed event should be raised for stop button.");
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

        [TestMethod]
        public void TurnOnHeater_WhenDoorIsClosed_CallsHardwareTurnOnHeater()
        {
            // Arrange
            _microwaveSimulator.SetDoorOpen(false); // Ensure door is closed

            // Act
            _microwaveSimulator.TurnOnHeater();

            // Assert
            _mockHardware.Verify(hw => hw.TurnOnHeater(), Times.Once);
        }

    }
}
