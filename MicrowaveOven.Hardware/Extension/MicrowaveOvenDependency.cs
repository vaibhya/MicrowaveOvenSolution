using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MicrowaveOven.Hardware.Enum;
using MicrowaveOven.Hardware.Microwave.Service.Impl;
using MicrowaveOven.Hardware.Oven.Service.Impl;
using MicrowaveOven.Hardware.Service;
using MicrowaveOven.Hardware.Service.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.Hardware.Extension
{
    public static class MicrowaveOvenDependency
    {
        public static IServiceCollection AddMicrowaveOven(this IServiceCollection services, IConfiguration configuration, Heater heaterType)
        {
            services.AddSingleton<IMicrowaveOvenFactory, MicrowaveOvenFactory>();
            services.AddSingleton<IMicrowaveOvenHW>(provider =>
            {
                var factory = provider.GetRequiredService<IMicrowaveOvenFactory>();
                return factory.GetHeater(heaterType);
            });
            return services;
        }
    }
}
