using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class MedAllo
    {
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)] // Define Task_Id as part of the composite primary key
        [ForeignKey("Medication")]
        public int Med_Id { get; set; }

        [Key]
        [Column(Order = 2)] // Define User_Id as part of the composite primary key
        [ForeignKey("User")]
        public string User_Id { get; set; }
        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime Date { get; set; }


        public string AnimalId { get; set; }

        public int Feed { get; set; }

        public MedAllo() { }

        public MedAllo(int id, int med_Id, string user_Id, string status, DateTime date, string animalId)
        {
            Id = id;
            Med_Id = med_Id;
            User_Id = user_Id;
            Status = status;
            Date = date;
            AnimalId = animalId;
        }

    }
}
