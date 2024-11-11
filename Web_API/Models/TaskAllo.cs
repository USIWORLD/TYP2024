using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;    

namespace FarmacyAPI.Models
{

    public class TaskAllo
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)] // Define Task_Id as part of the composite primary key
        [ForeignKey("Task")]    
        public int Task_Id { get; set; }

        [Key]
        [Column(Order = 2)] // Define User_Id as part of the composite primary key
        [ForeignKey("User")]
        public string User_Id { get; set; }
        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime Duedate { get; set; }


        public string AnimalId { get; set; }

        public int Feed { get; set; }
        public TaskAllo() { }


        public TaskAllo(int task_Id, string user_Id, string status, DateTime duedate, string animalID, int feed)
        {
            Task_Id = task_Id;
            User_Id = user_Id;
            Status = status;
            Duedate = duedate;
            AnimalId = animalID;
            Feed = feed;
        }


    }
}
