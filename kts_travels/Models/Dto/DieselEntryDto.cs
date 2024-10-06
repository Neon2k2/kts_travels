using System.ComponentModel.DataAnnotations;

namespace kts_travels.Models.Dto
{
    public class DieselEntryDto
    {
        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Vehicle number is required.")]
        [StringLength(50, ErrorMessage = "Vehicle number cannot exceed 50 characters.")]
        public string VehicleNo { get; set; }

        [Required(ErrorMessage = "Diesel liters are required.")]
        public double DieselLiters { get; set; }

        [Required(ErrorMessage = "Starting kilometers are required.")]
        public int StartingKm { get; set; }
    }
}
