﻿using LearningPlus.Models.Enums;
using LearningPlus.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningPlus.Models
{
   public class LearningPlusClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DayOfWeek DayOfWeek { get; set; }

        public TimeOfDay TimeOfDay { get; set; }

        public Disciplines Discipline{ get; set; }

        public ICollection<LearningPlusStudentsClasses> Students { get; set; }

        public LearningPlusUser Teacher { get; set; }

        public Room Room { get; set; }

        public bool Active { get; set; } = true;

        // public ICollection>Homework Homeworks { get; set; }

        // public ICollection>Homework LearningMaterials { get; set; }
    }
}
