using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class HealthCounts
    {
        public HealthCounts() { }

        [Required]

        public int key { get; set; }
        public int HealthyCount { get; set; }

        [Required]
        public int ExposedCount { get; set; }
        [Required]
        public int HighRiskCount { get; set; }

        public int fmd { get; set; }

    }
}
