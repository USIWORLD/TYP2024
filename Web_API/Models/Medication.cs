using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Dosage { get; set; }

        [Required]
        public string Frequency { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public decimal Cost { get; set; }

        public int Quantity { get; set; }



        public Medication() { }

        public Medication(int id, string name, string dosage, string frequency, string category, string type, decimal cost,int quantity)
        {
            Id = id;
            Name = name;
            Dosage = dosage;
            Frequency = frequency;
            Type = type;
            Category = category;
            Cost = cost;
            Quantity = quantity;
        }

    }
    }
