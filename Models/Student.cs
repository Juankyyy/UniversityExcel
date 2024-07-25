using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace University.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [JsonIgnore]
        public List<Inscription>? Inscriptions { get; set; }
    }
}