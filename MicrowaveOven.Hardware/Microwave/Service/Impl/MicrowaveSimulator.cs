﻿using MicrowaveOven.Hardware.Service;

namespace MicrowaveOven.Hardware.Microwave.Service.Impl
{
    internal class MicrowaveSimulator : IMicrowaveOvenHW
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
            Console.WriteLine("Microwave turnOff");
        }

        public void TurnOnHeater()
        {
            _doorOpen = false;
            Console.WriteLine("Microwave turnOn");

        }

    }
}
