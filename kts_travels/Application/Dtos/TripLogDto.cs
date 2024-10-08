namespace kts_travels.Application.Dtos
{
    public class TripLogDto
    {
        public int SerialNumber { get; set; }
        public int TripId { get; set; } // Unique identifier for the trip log
        public string VehicleNo { get; set; } // Vehicle number (comes from the related Vehicle entity)
        public int LocationId { get; set; } // Foreign key to reference the Site
        public DateTime Date { get; set; } // Date of the log entry
        public decimal DieselLiters { get; set; } // Amount of diesel filled
        public int StartingKm { get; set; } // Starting kilometer reading of the vehicle

    }
}
