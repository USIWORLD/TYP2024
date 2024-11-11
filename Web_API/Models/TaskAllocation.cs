using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmacyAPI.Models
{
    public class TaskAllocation
    {

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
        public DateTime Duedate { get; set;}

        public Task Task { get; set; }

        public User User { get; set; }

        public TaskAllocation() { }
    public TaskAllocation(int task_Id, string user_Id, string status, DateTime duedate)
        {
            Task_Id = task_Id;
            User_Id = user_Id;
            Status = status;
            Duedate = duedate;
        }


    }
}
