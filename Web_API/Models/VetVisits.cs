using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class VetVisits
    {
        public VetVisits() { }

        [Key]
        public int VisitID { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

        [Required]
        public int AnimalID { get; set; }

        [Required]
        public string Availability { get; set; }

        public decimal Cost { get; set; }

    }
}
