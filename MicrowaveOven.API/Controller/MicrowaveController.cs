using Microsoft.AspNetCore.Mvc;
using MicrowaveOven.DTO;
using MicrowaveOven.Enum;
using MicrowaveOven.Service;

namespace MicrowaveOven.API.Controllers
{
    [ApiController]
    [Route("heater")]
    [Produces(contentType: "application/json")]
    public class MicrowaveController : ControllerBase
    {
        private readonly IMicrowaveOvenSimulator _hadrware;
        private readonly IMicrowaveOvenFactory _factory;
        private readonly ITimerService _timerService;
        private readonly IMicrowaveOvenEventHandler _microwaveOvenEventHandler;

        public MicrowaveController(  IMicrowaveOvenSimulator hadrware, ITimerService timerService, IMicrowaveOvenEventHandler microwaveOvenEventHandler)
        {
            //_factory = factory;
            _hadrware = hadrware;
            _timerService = timerService;
            _microwaveOvenEventHandler = microwaveOvenEventHandler;
        }

        [HttpPost]
        [Route("door")]
        public IActionResult SimulateDoorActivity([FromBody] bool isOpen)
        {
            
            _hadrware.SetDoorOpen(isOpen);
            HeaterResponse heaterResponse = _microwaveOvenEventHandler.GetHeaterState();
            return Ok(heaterResponse);
        }

        [HttpPost]
        [Route("start")]
        public IActionResult SimulateStartButtonPress([FromBody] bool isStart)
        {
            _hadrware.SimulateStartButtonPress(isStart);
            HeaterResponse heaterResponse = _microwaveOvenEventHandler.GetHeaterState();
            return Ok(heaterResponse);
        }

        [HttpGet]
        [Route("state")]
        public IActionResult GetTime()
        {
            HeaterResponse heaterResponse = _microwaveOvenEventHandler.GetHeaterState();
            return Ok(heaterResponse);
        }
    }
    
}