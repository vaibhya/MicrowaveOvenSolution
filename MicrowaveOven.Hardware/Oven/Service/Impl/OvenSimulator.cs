using MicrowaveOven.Hardware.Service;

namespace MicrowaveOven.Hardware.Oven.Service.Impl
{
    internal class OvenSimulator : IMicrowaveOvenHW
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


        public void TurnOffHeater()
        {
            Console.WriteLine("Oven turnOff");
        }

        public void TurnOnHeater()
        {
            _doorOpen = false;
            Console.WriteLine("Oven turnOn");
        }

    }
}
