using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace University.Models
{
    public class Universities
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DeanName { get; set; }

        [JsonIgnore]
        public List<Inscription>? Inscriptions { get; set; }
        public List<Career>? Careers { get; set; }
    }
}