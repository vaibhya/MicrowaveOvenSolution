using MicrowaveOven.Enum;

namespace MicrowaveOven.Service
{
    public interface IMicrowaveOvenFactory
    {
        public IMicrowaveOvenHW GetHeater(Heater heater);
    }
}
