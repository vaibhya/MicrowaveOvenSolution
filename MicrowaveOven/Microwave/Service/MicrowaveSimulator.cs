using MicrowaveOven.Service;

namespace Microwave.Service
{
    public class MicrowaveSimulator : IMicrowaveOvenHW,IMicrowaveOvenSimulator
    {
        public event Action<bool> DoorOpenChanged;
        public event EventHandler StartButtonPressed;
        
        private bool _doorOpen;
        private bool _isHeaterStart;

        public bool DoorOpen
        {
            get { return _doorOpen; }
            set
            {
                _doorOpen = value;
            }
        }

        public bool StartButtonValue { 
            get { return _isHeaterStart; }
            set {
                _isHeaterStart = value;
            } 
        }

        public void TurnOffHeater()
        {
            Console.WriteLine("Microwave turnOff");
        }

        public void TurnOnHeater()
        {
            _doorOpen = false;
            Console.WriteLine("Microwave turnOn");

        }

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
                Console.WriteLine("Start button not pressed");
            }
        }


    }
}
