namespace kts_travels.Models
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LicenseNo { get; set; }
        public ICollection<DieselEntry> DieselEntries { get; set; }
    }
}
