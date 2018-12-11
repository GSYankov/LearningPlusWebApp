using LearningPlus.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningPlus.Models
{
    public class LearningPlusNewsTargetRole
    {
        [ForeignKey("News")]
        public Guid NewsId { get; set; }
        public virtual LearningPlusNews News { get; set; }

        public UserRoles TargetRole { get; set; }
    }
}
