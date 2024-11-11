using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class User
    {
        [Key]
        
        public string Id { get; set; }

        [Required]      
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int UserType {  get; set; }


        public User() { }
       public User(string id, string name, string surname, string email, string password, int userType)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            UserType = userType;
        }
    }

}
