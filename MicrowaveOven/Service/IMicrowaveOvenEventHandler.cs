using MicrowaveOven.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.Service
{
    public interface IMicrowaveOvenEventHandler
    {
        HeaterResponse GetHeaterState();
    }
}
