using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class ToDoList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        [Required]
        public string Notes { get; set; }

        [Required]
        public string Title { get; set; }

        public DateOnly TaskDate { get; set; }


        public ToDoList() { }


        public ToDoList(string note, string title,DateOnly date)
        {
            Notes = note;
            Title = title;
            TaskDate = date;    
        }
    }
}
