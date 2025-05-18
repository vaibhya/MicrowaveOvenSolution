using MicrowaveOven.Enum;
using MicrowaveOven.Service;
using MicrowaveOven.Service.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMicrowaveOvenFactory, MicrowaveOvenFactory>();
builder.Services.AddSingleton<ITimerService, TimerService>();
builder.Services.AddSingleton<MicrowaveOvenEventHandler>();
builder.Services.AddSingleton<IMicrowaveOvenSimulator>(provider =>
{
    var factory = provider.GetRequiredService<IMicrowaveOvenFactory>();
    return factory.GetHeater(Heater.Microwave);
});
builder.Services.AddSingleton<IMicrowaveOvenEventHandler, MicrowaveOvenEventHandler>();

var app = builder.Build();
//app.Services.GetRequiredService<MicrowaveOvenEventHandler>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseHttpsRedirection();

app.Run();


