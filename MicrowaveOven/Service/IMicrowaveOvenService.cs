using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.Service
{
    public interface IMicrowaveOvenService
    {
        void SimulateDoorActivity(bool isOpen);

        void SimulateStartButtonPress(bool isStart);

    }
}
