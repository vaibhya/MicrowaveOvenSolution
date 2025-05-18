using Microwave.Service;
using MicrowaveOven.Enum;
using MicrowaveOven.Oven.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.Service.Impl
{
    public class MicrowaveOvenFactory : IMicrowaveOvenFactory
    {
        private static IMicrowaveOvenSimulator _microwaveInstance;
        private static IMicrowaveOvenSimulator _ovenInstance;
        public IMicrowaveOvenSimulator GetHeater(Heater heater)
        {
            switch (heater)
            {
                case Heater.Microwave:
                    //return _microwaveInstance ?? (_microwaveInstance = new MicrowaveSimulator());
                    return  new MicrowaveSimulator();
                case Heater.Oven:
                    //return _ovenInstance ?? (_ovenInstance = new OvenSimulator());
                    return new OvenSimulator();
                default:
                    throw new ArgumentException("Invalid heater type");
            }
        }
    }
}
