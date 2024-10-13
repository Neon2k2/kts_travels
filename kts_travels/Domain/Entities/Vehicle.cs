using System.ComponentModel.DataAnnotations;

namespace kts_travels.SharedServices.Domain.Entities
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Vehicle number is required.")]
        [StringLength(17, ErrorMessage = "Vehicle number cannot exceed 17 characters.")]
        public string VehicleNo { get; set; }

        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public ICollection<TripLog> TripLogs { get; set; }

    }
}

