using MicrowaveOven.DTO;

namespace MicrowaveOven.Service.Impl
{
    public class MicrowaveOvenEventHandler : IMicrowaveOvenEventHandler
    {
        private readonly IMicrowaveOvenSimulator _microwaveOvenSimulator;
        private bool _isHeating;
        private HeaterResponse _heaterResponse;
        private ITimerService _timer;
        private readonly IMicrowaveOvenFactory _factory;

        public MicrowaveOvenEventHandler(ITimerService timerService, IMicrowaveOvenSimulator microwaveOvenSimulator)
        {
            //_microwaveOvenSimulator = _factory.GetHeater(Heater.Microwave);
            _microwaveOvenSimulator = microwaveOvenSimulator;
            _timer = timerService;
            //microwave is hardcoded this will be configurable in the future

            // Subscribe to the Action<bool> event signature as provided by the client
            _microwaveOvenSimulator.DoorOpenChanged += OnDoorOpenChanged;
            _microwaveOvenSimulator.StartButtonPressed += OnStartButtonPressed;
            _timer.IsTimeOver += OnTimeUp;
            _heaterResponse = new HeaterResponse();
        }

        private void OnTimeUp(bool isTimeUp)
        {
            _microwaveOvenSimulator.TurnOffHeater();
            _isHeating = false;
            _heaterResponse.IsHeaterOn = false;
        }

        private void OnDoorOpenChanged(bool isOpen)
        {

            if (isOpen)
            {
                _microwaveOvenSimulator.TurnOffHeater();
                _isHeating = false;
                _heaterResponse.IsLightOn = true;
                _heaterResponse.IsHeaterOn = false;
                _heaterResponse.IsDoorOpen = true;

                _timer.Stop();
                _heaterResponse.TimeLeft = _timer.GetTime();
                Console.WriteLine("Door is open, microwave is turned off.");
            }
            else
            {
                
                _heaterResponse.IsLightOn = false;
                _heaterResponse.IsHeaterOn = false;
                _heaterResponse.IsDoorOpen = false;
                
                
                //add logic if time is there is door false add seperate message
                Console.WriteLine("Door is closed, microwave is ready to use.");
            }
        }

        private void OnStartButtonPressed(object sender, EventArgs e)
        {

            if (_microwaveOvenSimulator.DoorOpen)
            {
                Console.WriteLine("Cannot start microwave, door is open.");
                return;
            }
            if (_microwaveOvenSimulator.StartButtonValue)
            {
                _microwaveOvenSimulator.TurnOffHeater();
                _isHeating = false;

                _heaterResponse.IsHeaterOn = false;
                _heaterResponse.IsLightOn = false;
                _heaterResponse.IsDoorOpen = false;
                _timer.Stop();
                _heaterResponse.TimeLeft = _timer.GetTime();

                Console.WriteLine("Microwave is turned off.");
            }

            else
            {
                _microwaveOvenSimulator.TurnOnHeater();
                //_timer.Start();
                _timer.AddTime(60); // Add 60 seconds to the timer
                _heaterResponse.TimeLeft = _timer.GetTime();
                _heaterResponse.IsHeaterOn = true;
                _heaterResponse.IsLightOn = false;
                _heaterResponse.IsDoorOpen = false;

                _isHeating = true;
                Console.WriteLine("Microwave is turned on.");
            }
        }

        public HeaterResponse GetHeaterState()
        {
            int timeRemaining = _timer.GetTime();
            _heaterResponse.TimeLeft = timeRemaining;
            return _heaterResponse;
        }
    }
}
