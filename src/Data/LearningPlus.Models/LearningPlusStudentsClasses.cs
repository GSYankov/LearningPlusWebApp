using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningPlus.Models
{
    public class LearningPlusStudentsClasses
    {
        [ForeignKey("Class")]
        public Guid ClassId { get; set; }
        public virtual LearningPlusClass Class { get; set; }

        [ForeignKey("Student")]
        public string StudentId { get; set; }
        public virtual LearningPlusUser Student { get; set; }
    }
}
