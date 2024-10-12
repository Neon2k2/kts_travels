namespace kts_travels.Core.Application.Dtos
{
    public class VehicleSummaryDto
    {
        public int SummaryId { get; set; }
        public int SRNo { get; set; } // Unique identifier (serial number)
        public DateTime Month { get; set; }
        public string VehicleNo { get; set; }
        public int LocationId { get; set; }// Vehicle number
        public int TotalDaysFilledDiesel { get; set; } // Total days diesel was filled in the vehicle
        public decimal TotalDiesel { get; set; } // Total diesel filled (in liters)
        public int OpeningKms { get; set; } // Initial kilometer reading for the vehicle
        public int ClosingKms { get; set; } // Final kilometer reading for the vehicle
        public int TotalKmRun { get; set; } // Total kilometers run
        public decimal Average { get; set; } // Average kilometers per liter (TotalKmRun / TotalDiesel)
    }
}
