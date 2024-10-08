using kts_travels.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace kts_travels.Domain.Entities
{
    public class VehicleSummary
    {
        [Key]
        public int SummaryId { get; set; }
        public int SRNo { get; set; } // Unique identifier (serial number)
        public DateTime Month { get; set; }
        public int VehicleId { get; set; } // Foreign key to reference the Vehicle
        public Vehicle Vehicle { get; set; } // Navigation property for the Vehicle
        public int TotalDaysFilledDiesel { get; set; } // Total days diesel was filled in the vehicle
        public decimal TotalDiesel { get; set; } // Total diesel filled (in liters)
        public int OpeningKms { get; set; } // Initial kilometer reading for the vehicle
        public int ClosingKms { get; set; } // Final kilometer reading for the vehicle
        public int TotalKmRun { get; set; } // Total kilometers run
        public decimal Average { get; set; } // Average kilometers per liter (TotalKmRun / TotalDiesel)

        // Foreign key to the Vehicle table

    }
}
