using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Task_Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Category { get; set; }


        public Task() { }
        public Task(int id, string tasK_Name, string description, string category)
        {
            Id = id;
            Task_Name = tasK_Name;
            Description = description;
            Category = category;
        }
    }
}
