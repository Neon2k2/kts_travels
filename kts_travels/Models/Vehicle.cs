namespace kts_travels.Models
{
    public class Vehicle
    {
        public string VehicleNo { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public ICollection<DieselEntry> DieselEntries { get; set; }

    }
}
