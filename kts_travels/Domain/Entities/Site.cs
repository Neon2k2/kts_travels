using System.ComponentModel.DataAnnotations;

namespace kts_travels.Domain.Entities
{
    public class Site
    {
        [Key]
        public int SiteId { get; set; }
        public string SiteName { get; set; }

        public ICollection<TripLog> TripLogs { get; set; }
    }
}
