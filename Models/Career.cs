using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace University.Models
{
    public class Career
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int UniversityId { get; set; }
        public Universities? University { get; set; }

        [JsonIgnore]
        public List<Inscription>? Inscriptions { get; set; }
        public List<Subject>? Subjects { get; set; }
    }
}