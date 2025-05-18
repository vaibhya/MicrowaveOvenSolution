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
        private readonly IMicrowaveOvenSimulator _hadrwareSimulator;
        private readonly IMicrowaveOvenEventHandler _microwaveOvenEventHandler;

        public MicrowaveController(  IMicrowaveOvenSimulator hadrwareSimulator, IMicrowaveOvenEventHandler microwaveOvenEventHandler)
        {
            _hadrwareSimulator = hadrwareSimulator;
            _microwaveOvenEventHandler = microwaveOvenEventHandler;
        }

        [HttpPost]
        [Route("door")]
        public IActionResult SimulateDoorOpenActivity()
        {
            
            _hadrwareSimulator.SetDoorOpen(true);
            HeaterResponse heaterResponse = _microwaveOvenEventHandler.GetHeaterState();
            return Ok(heaterResponse);
        }
        [HttpDelete]
        [Route("door")]
        public IActionResult SimulateDoorCloseActivity()
        {

            _hadrwareSimulator.SetDoorOpen(false);
            HeaterResponse heaterResponse = _microwaveOvenEventHandler.GetHeaterState();
            return Ok(heaterResponse);
        }

        [HttpPost]
        [Route("start")]
        public IActionResult SimulateStartButtonPress()
        {
            _hadrwareSimulator.SimulateStartButtonPress(true);
            HeaterResponse heaterResponse = _microwaveOvenEventHandler.GetHeaterState();
            return Ok(heaterResponse);
        }

        [HttpDelete]
        [Route("start")]
        public IActionResult SimulateStopButtonPress()
        {
            _hadrwareSimulator.SimulateStartButtonPress(false);
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