using MicrowaveOven.DTO;

namespace MicrowaveOven.Service
{
    public interface IMicrowaveOvenEventHandler
    {
        HeaterResponse GetHeaterState();
    }
}
