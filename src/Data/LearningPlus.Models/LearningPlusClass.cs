using LearningPlus.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningPlus.Models
{
    public class LearningPlusClass
    {
        public LearningPlusClass()
        {
            this.StudentsEnrolled = new HashSet<LearningPlusClassesStudents>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DaysOfWeek DayOfWeek { get; set; }

        public TimeOfDay TimeOfDay { get; set; }

        public Disciplines Discipline{ get; set; }

        public ICollection<LearningPlusClassesStudents> StudentsEnrolled { get; set; }

        public virtual LearningPlusUser Teacher { get; set; }

        public Room Room { get; set; }

        public bool Active { get; set; } = true;

        public ICollection<LearningPlusHomeWork> Homeworks { get; set; }

        // public ICollection>Homework LearningMaterials { get; set; }
    }
}
