using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningPlus.Models
{
    public class LearningPlusChat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public LearningPlusUser Sender { get; set; }

        public DateTime Time { get; set; } = DateTime.UtcNow;

        public string Message { get; set; }
    }
}
