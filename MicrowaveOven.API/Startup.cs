using MicrowaveOven.Enum;
using MicrowaveOven.Service;
using MicrowaveOven.Service.Impl;

namespace MicrowaveOven.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<IMicrowaveOvenFactory, MicrowaveOvenFactory>();
            services.AddSingleton<ITimerService, TimerService>();
            services.AddSingleton<MicrowaveOvenEventHandler>();
            services.AddSingleton<IMicrowaveOvenSimulator>(provider =>
            {
                var factory = provider.GetRequiredService<IMicrowaveOvenFactory>();
                return factory.GetHeater(Heater.Microwave);
            });
            services.AddSingleton<IMicrowaveOvenEventHandler, MicrowaveOvenEventHandler>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
