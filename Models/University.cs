using System.ComponentModel.DataAnnotations;

namespace University.Models
{
    public class Universities
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DeanName { get; set; }
    }
}