using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace University.Models
{
    public class Semester
    {
        public int Id { get; set; }

        [Required]
        public int SemesterNumber { get; set; }

        [Required]
        public int Year { get; set; }

        [JsonIgnore]
        public List<Subject>? Subjects { get; set; }
    }
}