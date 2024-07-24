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

        [Required]
        public int CareerId { get; set; }

        [Required]
        public int SemesterId { get; set; }
    }
}