using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveOven.DTO
{
    public  class HeaterResponse
    {
        public bool IsHeaterOn { get; set; }
        public bool IsDoorOpen { get; set; }
        public int TimeLeft { get; set; }
        public bool IsLightOn { get; set; }
        
    }
}
