using MicrowaveOven.Enum;

namespace MicrowaveOven.Service
{
    public interface IMicrowaveOvenFactory
    {
        public IMicrowaveOvenSimulator GetHeater(Heater heater);
    }
}
