using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        [Required]
        public int CareerId { get; set; }
        public Career? Career { get; set; }

        [Required]
        public int SemesterId { get; set; }
        public Semester? Semester { get; set; }
    }
}