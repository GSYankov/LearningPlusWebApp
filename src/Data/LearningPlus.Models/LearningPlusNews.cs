using LearningPlus.Web.Models;
using LearningPlus.Web.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearningPlus.Models
{
    public class LearningPlusNews
    {
        public LearningPlusNews()
        {
            this.TargetRoles = new HashSet<LearningPlusNewsTargetRole>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public LearningPlusUser Creator { get; set; }

        public string Message { get; set; }

        public ICollection<LearningPlusNewsTargetRole> TargetRoles { get; set; }

        public DateTime ExpiresOn { get; set; } = DateTime.UtcNow.AddDays(14);
    }
}
