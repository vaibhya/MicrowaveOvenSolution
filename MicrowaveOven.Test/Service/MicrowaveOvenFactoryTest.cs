using Microwave.Service;
using MicrowaveOven.Enum;
using MicrowaveOven.Oven.Service;
using MicrowaveOven.Service;
using MicrowaveOven.Service.Impl;

namespace MicrowaveOven.Test.Service
{
    [TestClass]
    public sealed class MicrowaveOvenFactoryTest
    {
        private IMicrowaveOvenFactory _factory;

        [TestInitialize]
        public void Setup()
        {
            _factory = new MicrowaveOvenFactory();
        }

        [TestMethod]
        public void GetHeater_WhenHeaterIsMicrowave_ReturnsMicrowaveSimulator()
        {
            // Act
            var result = _factory.GetHeater(Heater.Microwave);

            // Assert
            Assert.IsInstanceOfType(result, typeof(MicrowaveSimulator));
        }

        [TestMethod]
        public void GetHeater_WhenHeaterIsOven_ReturnsOvenSimulator()
        {
            // Act
            var result = _factory.GetHeater(Heater.Oven);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OvenSimulator));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetHeater_WhenHeaterIsInvalid_ThrowsArgumentException()
        {
            // Act
            _factory.GetHeater((Heater)999);
        }
    }
}
