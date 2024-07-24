using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Semester
    {
        public int Id { get; set; }

        [Required]
        public int SemesterNumber { get; set; }

        [Required]
        public int Year { get; set; }
    }
}