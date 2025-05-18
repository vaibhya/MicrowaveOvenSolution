using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.Service
{
    public interface ITimerService
    {
        public void Start();
        public void Stop();
        public void Resume();
        public void Reset();
        public void AddTime(int seconds);
        public int GetTime(); 
        public event Action<bool> IsTimeOver;
    }
}
