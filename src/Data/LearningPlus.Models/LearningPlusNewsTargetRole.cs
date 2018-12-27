using LearningPlus.Web.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningPlus.Models
{
    public class LearningPlusNewsTargetRole
    {
        [ForeignKey("News")]
        public Guid NewsId { get; set; }
        public LearningPlusNews News { get; set; }

        public virtual UserRoles TargetRole { get; set; }
    }
}
