using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Inscription
    {
        public int Id { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public int UniversityId { get; set; }

        [Required]
        public int CareerId { get; set; }
    }
}