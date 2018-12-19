using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningPlus.Models
{
    public class LearningPlusHomeWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string BlobLink { get; set; }

        public LearningPlusUser Student { get; set; }

        public LearningPlusClass Course { get; set; }

        public string Resolutions { get; set; }
    }
}
