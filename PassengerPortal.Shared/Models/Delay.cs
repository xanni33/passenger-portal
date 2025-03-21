namespace PassengerPortal.Shared.Models
{
    public class Delay
    {
        public int Id { get; set; }
        public string TrainNumber { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public int DelayInMinutes { get; set; } // 5, 10, 15
    }
}

