namespace MicrowaveOven.Service
{
    public interface IMicrowaveOvenSimulator : IMicrowaveOvenHW
    {
        /// <summary>
        /// Simulates the door activity of the microwave oven.
        /// </summary>
        /// <param name="isOpen">True if the door is open, false if it is closed.</param>
        void SetDoorOpen(bool isOpen);

        /// <summary>
        /// Simulates the start button press of the microwave oven.
        /// </summary>
        /// <param name="isStart">True if the start button is pressed, false otherwise.</param>
        void SimulateStartButtonPress(bool isStart);

        bool StartButtonValue { get; set; }


    }
}