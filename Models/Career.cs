using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Career
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // [Required]
        // public string Semester { get; set; }

        // [Required]
        // public string Year { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int UniversityId { get; set; }
    }
}