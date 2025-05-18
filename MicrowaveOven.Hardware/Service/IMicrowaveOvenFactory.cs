using MicrowaveOven.Hardware.Enum;

namespace MicrowaveOven.Hardware.Service
{
    public interface IMicrowaveOvenFactory
    {
        public IMicrowaveOvenHW GetHeater(Heater heater);
    }
}
