using System.ComponentModel.DataAnnotations;

namespace kts_travels.Domain.Entities
{
    public class TripLog
    {
        [Key]
        public int TripId { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }


        [Required(ErrorMessage = "Location is required.")]
        public int LocationId { get; set; }
        public Site Location { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
        //[Required(ErrorMessage = "Vehicle number is required.")]
        //[StringLength(17, ErrorMessage = "Vehicle number cannot exceed 17 characters.")]
        //public string VehicleNO { get; set; }
        [Required(ErrorMessage = "Diesel liters are required.")]
        public decimal DieselLiters { get; set; }
        [Required(ErrorMessage = "Starting kilometers are required.")]
        public int StartingKm { get; set; }


    }
}
