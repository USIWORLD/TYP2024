 using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class LoginModel
    {
        [Key]

        public string Id { get; set; }

        [Required]
        public string Password { get; set; }

        public LoginModel() { }

        public LoginModel(string id,string pass) 
        { 
            Id = id;
            Password = pass;
        }
    }

}
