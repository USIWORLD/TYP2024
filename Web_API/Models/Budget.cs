using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FarmacyAPI.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public decimal Price { get; set; }


        [Required]
        public string Month { get; set; }

        public Budget() { }

        public Budget(int id, string category, decimal price,string month)
        {
            Id = id;    
            Category = category;
            Price = price;
            Month = month;

        }

    }
}
