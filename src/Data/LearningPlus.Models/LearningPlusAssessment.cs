using LearningPlus.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningPlus.Models
{
    public class LearningPlusAssessment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public double Mark { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public LearningPlusUser Child { get; set; }

        [Required]
        public Disciplines Course { get; set; }
    }
}
