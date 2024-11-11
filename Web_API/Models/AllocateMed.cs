using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class AllocateMed
    {

        public int Id { get; set; }

        [Key]
        [Column(Order = 1)] // Define Task_Id as part of the composite primary key
        public string Med_Id { get; set; }

        [Key]
        [Column(Order = 2)] // Define User_Id as part of the composite primary key
        [ForeignKey("User")]
        public string User_Id { get; set; }
        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime Date { get; set; }


        public string AnimalId { get; set; }

        public string MedNotes { get; set; }

        public AllocateMed() { }

        public AllocateMed(int id, string med_Id, string user_Id, string status, DateTime date, string animalId, string medNotes)
        {
            Id = id;
            Med_Id = med_Id;
            User_Id = user_Id;
            Status = status;
            Date = date;
            AnimalId = animalId;
            MedNotes = medNotes;
        }
    }
}
