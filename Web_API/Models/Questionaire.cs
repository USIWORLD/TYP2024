
using FarmacyAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmacyAPI.Models
{
    public class Questionaire
    {
        public readonly ApplicationDbContext context;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public DateTime DateAcquired { get; set; }

        [Required]
        public int Q_One { get; set; }

        [Required]
        public int Q_Two { get; set; }

        [Required]
        public int Q_Three { get; set; }

        [Required]
        public int Q_Four { get; set; }
        [Required]
        public int Q_Five { get; set; }
        [Required]
        public int Q_Six { get; set; }
        [Required]
        public int Q_Seven { get; set; }
        [Required]
        public int Q_Eight { get; set; }
        [Required]
        public int Q_Nine { get; set; }
        [Required]
        public int Q_Ten { get; set; }
        [Required]
        public string U_ID { get; set; }
        [Required]
        public int Cid { get; set; }


        public Questionaire() { }

        public Questionaire(ApplicationDbContext db) 
        {
            context = db;
        }
        public Questionaire(DateTime dat, int q1, int q2, int q3, int q4, int q5, int q6, int q7, int q8, int q9, int q10,string uID, int cID)
        {
            DateAcquired = dat;
            Q_One = q1;
            Q_Two = q2;
            Q_Three = q3;
            Q_Four = q4;
            Q_Five = q5;
            Q_Six = q6;
            Q_Seven = q7;
            Q_Eight = q8;
            Q_Nine = q9;
            Q_Ten = q10;
            U_ID = uID;
            Cid = cID;
        }
    }
}
