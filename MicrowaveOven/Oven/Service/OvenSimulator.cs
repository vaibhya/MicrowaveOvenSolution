using MicrowaveOven.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.Oven.Service
{
    public class OvenSimulator : IMicrowaveOvenHW,IMicrowaveOvenSimulator
    {
        public event Action<bool> DoorOpenChanged;
        public event EventHandler StartButtonPressed;
        private bool _doorOpen;

        public bool DoorOpen
        {
            get { return _doorOpen; }
            set
            {
                _doorOpen = value;
            }
        }

        public bool StartButtonValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void TurnOffHeater()
        {
            Console.WriteLine("Oven turnOff");
        }

        public void TurnOnHeater()
        {
            _doorOpen = false;
            Console.WriteLine("Oven turnOn");
        }

        public void SetDoorOpen(bool isOpen)
        {
            _doorOpen = isOpen;
            DoorOpenChanged?.Invoke(_doorOpen);
        }

        public void SimulateStartButtonPress(bool isStart)
        {
            StartButtonPressed?.Invoke(this, EventArgs.Empty);
        }
    }
}
