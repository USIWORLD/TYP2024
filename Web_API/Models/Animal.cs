using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public string Gender { get; set; }


        [Required]
        public string Breed { get; set; }

        [Required]
        public string Colour { get; set; }

        [Required]
        public decimal A_Weight { get; set; }

        [Required]
        public decimal Height { get; set; }

        [Required]
        public string HealthStatus { get; set; }

        [Required]
        public DateTime DateAcquired { get; set; }

        [Required]
        public int Age { get; set; }

        public int Kraal { get; set; }

        public string AppetiteStatus { get; set; }

        public decimal New_Weight { get; set; }



        public Animal() { }
        public Animal(int id,string name, string species, string gender, string breed, string colour, decimal weight, decimal height, string healthStatus, DateTime dateAcquired, int age, int kraal, string appetiteStatus,decimal new_weight)
        {
            Id = id;
            Species = species;
            Gender = gender;
            Breed = breed;
            Colour = colour;
            A_Weight = weight;
            Height = height;
            HealthStatus = healthStatus;
            DateAcquired = dateAcquired;
            Age = age;
            Kraal = kraal;
            AppetiteStatus = appetiteStatus;
            New_Weight = new_weight;
            Name = name;
        }




    }
}
