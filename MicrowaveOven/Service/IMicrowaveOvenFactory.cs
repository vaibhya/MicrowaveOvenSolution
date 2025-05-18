using MicrowaveOven.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.Service
{
    public interface IMicrowaveOvenFactory
    {
        public IMicrowaveOvenSimulator GetHeater(Heater heater);
    }
}
