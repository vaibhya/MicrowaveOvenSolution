
using MicrowaveOven.Hardware.Service;

namespace MicrowaveOven.Service.Impl
{
    public class MicrowaveOvenSimulator : IMicrowaveOvenSimulator
    {
        private readonly IMicrowaveOvenHW _hardware;
        private bool _isHeaterStart;
        private bool _doorOpen;
        public event Action<bool> DoorOpenChanged;
        public event EventHandler StartButtonPressed;

        public MicrowaveOvenSimulator(IMicrowaveOvenHW hardware)
        { 
            _hardware = hardware;
            _hardware.DoorOpenChanged += (open) => DoorOpenChanged?.Invoke(open);
            _hardware.StartButtonPressed += (s, e) => StartButtonPressed?.Invoke(s, e);
        }

        public bool StartButtonValue
        {
            get { return _isHeaterStart; }
            set
            {
                _isHeaterStart = value;
            }
        }


        public bool DoorOpen { get => _doorOpen; set => _doorOpen = value; }

        public void SetDoorOpen(bool isOpen)
        {
            _doorOpen = isOpen;
            DoorOpenChanged?.Invoke(_doorOpen);
        }

        public void SimulateStartButtonPress(bool isStart)
        {
            if (isStart)
            {
                _isHeaterStart = isStart;
                StartButtonPressed?.Invoke(this, EventArgs.Empty);
                Console.WriteLine("Start button pressed");
            }
            else
            {
                _isHeaterStart = isStart;
                StartButtonPressed?.Invoke(this, EventArgs.Empty);
                Console.WriteLine("Start button not pressed");
            }
        }

        public void TurnOnHeater()
        {
            if (_doorOpen)
            {
                throw new Exception("Door is Open");
            }
            _hardware.TurnOnHeater();
        }

        public void TurnOffHeater()
        {
            _hardware.TurnOffHeater();
        }
    }
}
