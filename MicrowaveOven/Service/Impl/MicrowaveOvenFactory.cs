using Microwave.Service;
using MicrowaveOven.Enum;
using MicrowaveOven.Oven.Service;

namespace MicrowaveOven.Service.Impl
{
    public class MicrowaveOvenFactory : IMicrowaveOvenFactory
    {
        public IMicrowaveOvenSimulator GetHeater(Heater heater)
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
