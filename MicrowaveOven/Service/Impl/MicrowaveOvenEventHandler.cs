using MicrowaveOven.DTO;

namespace MicrowaveOven.Service.Impl
{
    public class MicrowaveOvenEventHandler : IMicrowaveOvenEventHandler
    {
        private readonly IMicrowaveOvenSimulator _microwaveOvenSimulator;

        private HeaterResponse _heaterResponse;
        private ITimerService _timer;


        public MicrowaveOvenEventHandler(ITimerService timerService, IMicrowaveOvenSimulator microwaveOvenSimulator)
        {
            _microwaveOvenSimulator = microwaveOvenSimulator;
            _timer = timerService;

            // Subscribe to events published by Microwave/oven
            _microwaveOvenSimulator.DoorOpenChanged += OnDoorOpenChanged;
            _microwaveOvenSimulator.StartButtonPressed += OnStartButtonPressed;
            _timer.IsTimeOver += OnTimeUp;

            _heaterResponse = new HeaterResponse();
        }

        /// <summary>
        /// Handles the event when the timer expires.
        /// </summary>
        /// <remarks>This method stops the heating process by turning off the heater and updating the
        /// internal state.</remarks>
        /// <param name="isTimeUp">A value indicating whether the timer has reached its end.  This parameter is expected to be <see
        /// langword="true"/> when the timer expires.</param>
        private void OnTimeUp(bool isTimeUp)
        {
            _microwaveOvenSimulator.TurnOffHeater();

            _heaterResponse.IsHeaterOn = false;
        }

        /// <summary>
        /// Handles the state change of the microwave oven door.
        /// </summary>
        /// <remarks>When the door is opened, the microwave stops heating, the timer is paused, and the
        /// internal state is updated  to reflect that the door is open. When the door is closed, the microwave is set
        /// to a ready state.</remarks>
        /// <param name="isOpen">A boolean value indicating whether the door is open.  <see langword="true"/> if the door is open; otherwise,
        /// <see langword="false"/>.</param>
        private void OnDoorOpenChanged(bool isOpen)
        {

            if (isOpen)
            {
                _microwaveOvenSimulator.TurnOffHeater();

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
                Console.WriteLine("Door is closed, microwave is ready to use.");
            }
        }

        /// <summary>
        /// Handles the event triggered when the start button of the microwave is pressed.
        /// </summary>
        /// <remarks>This method checks the state of the microwave door and the current heating status to
        /// determine  whether to start or stop the microwave. If the door is open, the microwave cannot be started.  If
        /// the microwave is already running, pressing the start button will stop it. Otherwise, pressing  the start
        /// button will start the microwave and add 60 seconds to the timer.</remarks>
        /// <param name="sender">The source of the event, typically the start button.</param>
        /// <param name="e">The event data associated with the button press.</param>
        private void OnStartButtonPressed(object sender, EventArgs e)
        {

            if (_microwaveOvenSimulator.DoorOpen)
            {
                Console.WriteLine("Cannot start microwave, door is open.");
                return;
            }
            if (_microwaveOvenSimulator.StartButtonValue)
            {
                _microwaveOvenSimulator.TurnOnHeater();
                _timer.AddTime(60); // Add 60 seconds to the timer
                _heaterResponse.TimeLeft = _timer.GetTime();
                _heaterResponse.IsHeaterOn = true;
                _heaterResponse.IsLightOn = false;
                _heaterResponse.IsDoorOpen = false;


            }
            else
            {
                _microwaveOvenSimulator.TurnOffHeater();
                _heaterResponse.IsHeaterOn = false;
                _heaterResponse.IsLightOn = false;
                _heaterResponse.IsDoorOpen = false;
                _timer.Stop();
                _heaterResponse.TimeLeft = _timer.GetTime();


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
