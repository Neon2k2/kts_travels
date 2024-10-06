namespace kts_travels.Models.Dto
{
    public class DieselAverageDto
    {
        public string VehicleNo { get; set; }
        public double TotalDieselLiters { get; set; }
        public int OpeningKms { get; set; }
        public int ClosingKms { get; set; }
        public int TotalKmRun { get; set; }
        public double Average { get; set; }

    }
}
