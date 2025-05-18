using MicrowaveOven.Hardware.Enum;
using MicrowaveOven.Hardware.Microwave.Service.Impl;
using MicrowaveOven.Hardware.Oven.Service.Impl;

namespace MicrowaveOven.Hardware.Service.Impl
{
    public class MicrowaveOvenFactory : IMicrowaveOvenFactory
    {
        public IMicrowaveOvenHW GetHeater(Heater heater)
        {
            switch (heater)
            {
                case Heater.Microwave:
                    return  new MicrowaveSimulator();
                case Heater.Oven:
                    return new OvenSimulator();
                default:
                    throw new ArgumentException("Invalid heater type");
            }
        }
    }
}
