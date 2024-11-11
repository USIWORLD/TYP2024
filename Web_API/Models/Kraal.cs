using System.ComponentModel.DataAnnotations;

namespace FarmacyAPI.Models
{
    public class Kraal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Size { get; set; }

        [Required]
        public int Sanitizer { get; set; }

        [Required]
        public int SizeSanitized { get; set; }

        [Required]
        public int SizeUnsanitized { get; set; }

        public Kraal() { }

        public Kraal(int id, string name, int size, int sanitizer, int sizeSanitized, int sizeUnsanitized)
        {
            Id = id;
            Name = name;
            Size = size;
            Sanitizer = sanitizer;
            SizeSanitized = sizeSanitized;
            SizeUnsanitized = sizeUnsanitized;
        }
    }
}
