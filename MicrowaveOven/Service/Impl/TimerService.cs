using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.Service.Impl
{
    public class TimerService : ITimerService
    {
        private readonly Timer _timer;
        private int _remainingSeconds;

        public event Action<bool> IsTimeOver;

        public TimerService()
        {
            _timer = new Timer(TimerTick, null, Timeout.Infinite, 1000);
        }
        public void AddTime(int seconds)
        {
            _timer.Change(0, 1000);
            _remainingSeconds += seconds;
        }

        public int GetTime()
        {
            return _remainingSeconds;
        }

        public void Reset()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _remainingSeconds = 0;
        }

        public void Start()
        {
            _timer.Change(0, 1000);
            _remainingSeconds = 60; // Reset the timer when starting
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        public void Resume()
        {
            _timer.Change(0, 1000);
        }
        private void TimerTick(object state)
        {
            if (_remainingSeconds <= 0)
            {
                _timer.Change(Timeout.Infinite, Timeout.Infinite);
                IsTimeOver?.Invoke(true);
            }
            else
            {
                _remainingSeconds--;
            }
        }
    }
}
