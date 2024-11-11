using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmacyAPI.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required]
        public DateTime DateGenerated { get; set; }

        [Required]
        public int HealthyCount { get; set; }


        [Required]

        public int HighRiskCount { get; set; }


        [Required]

        public int ExposedCount { get; set; }

        [Required]
        public string Decision { get; set;}


     
            
        [ForeignKey("Animal")]
        public int AnimalId { get; set; }

        public Animal Animal { get; set; }

        public Report() { }

        public Report(int id, DateTime dateGenerated, int healthyCount, int highRiskCount, int exposedCount, string decision, int a_ID)
        {
            ReportId = id;
            DateGenerated = dateGenerated;
            HealthyCount = healthyCount;
            HighRiskCount = highRiskCount;
            ExposedCount = exposedCount;
            Decision = decision;
            AnimalId = a_ID;
        }
    }
}
