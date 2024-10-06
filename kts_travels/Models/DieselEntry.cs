namespace kts_travels.Models
{
    public class DieselEntry
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public Vehicle VehicleNo { get; set; }
        public double DieselLiters { get; set; }
        public int StartingKm { get; set; }
    }
}
