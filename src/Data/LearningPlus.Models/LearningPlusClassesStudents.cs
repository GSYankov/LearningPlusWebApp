using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningPlus.Models
{
   public class LearningPlusClassesStudents
    {
        public Guid ClassId { get; set; }
        public LearningPlusClass Class { get; set; }

        public string StudentId { get; set; }
        public LearningPlusUser Student { get; set; }
    }
}
