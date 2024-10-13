using kts_travels.SharedServices.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace kts_travels.SharedServices.Domain.Entities
{
    public class VehicleSummary
    {
        [Key]
        public int SummaryId { get; set; }
        public int SRNo { get; set; }
        public DateTime Month { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int LocationId { get; set; }
        public Site Location { get; set; }
        public int TotalDaysFilledDiesel { get; set; }
        public decimal TotalDiesel { get; set; }
        public int OpeningKms { get; set; }
        public int ClosingKms { get; set; }
        public int TotalKmRun { get; set; }
        public decimal Average { get; set; }

        // Constructor for easy initialization
        public VehicleSummary(int vehicleId, int locationId, DateTime month,
                              int totalDaysFilledDiesel, decimal totalDiesel,
                              int openingKms, int closingKms,
                              int totalKmRun, decimal average)
        {
            VehicleId = vehicleId;
            LocationId = locationId;
            Month = month;
            TotalDaysFilledDiesel = totalDaysFilledDiesel;
            TotalDiesel = totalDiesel;
            OpeningKms = openingKms;
            ClosingKms = closingKms;
            TotalKmRun = totalKmRun;
            Average = average;
        }

        // Method to update summary fields
        public void UpdateSummary(int totalDaysFilledDiesel, decimal totalDiesel,
                                  int openingKms, int closingKms,
                                  int totalKmRun, decimal average)
        {
            TotalDaysFilledDiesel = totalDaysFilledDiesel;
            TotalDiesel = totalDiesel;
            OpeningKms = openingKms;
            ClosingKms = closingKms;
            TotalKmRun = totalKmRun;
            Average = average;
        }

        public override string ToString()
        {
            return $"SummaryId: {SummaryId}, Month: {Month.ToString("MMMM yyyy")}, VehicleId: {VehicleId}, TotalKmRun: {TotalKmRun}, Average: {Average}";
        }

    }


}
