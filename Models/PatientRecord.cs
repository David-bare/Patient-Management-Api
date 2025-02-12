using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientManagement.Models
{
    public class PatientRecord
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public int Age { get; set; }
        public string? BloodGroup { get; set; }
        public string? Genotype { get; set; }
        public string? Sickness { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;
        [ForeignKey("Patient")]
        public int PatientId { get; set; }
    }
}
