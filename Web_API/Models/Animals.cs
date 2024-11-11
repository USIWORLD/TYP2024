using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmacyAPI.Models
{
    public class Animals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string HealthStatus { get; set; }

        [Required]
        public decimal A_Weight { get; set; }

        [Required]
        public decimal Height { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public DateTime DateAcquired { get; set; }

        

        public Animals() { }
        public Animals(string species, string gender, string healthStatus, decimal weight, decimal height, int age, DateTime dateAcquired)
        {
            Species = species;
            Gender = gender;
            HealthStatus = healthStatus;
            A_Weight = weight;
            Height = height;
            Age = age;
            DateAcquired = dateAcquired;
            
        }




    }
}

