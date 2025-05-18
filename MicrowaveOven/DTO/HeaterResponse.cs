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
