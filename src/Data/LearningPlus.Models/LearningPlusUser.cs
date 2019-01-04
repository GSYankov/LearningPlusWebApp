using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearningPlus.Models
{
    public class LearningPlusUser : IdentityUser
    {
        public LearningPlusUser()
        {
            this.Children = new HashSet<LearningPlusUser>();
            this.ClassesEnrolled = new HashSet<LearningPlusClassesStudents>();
        }

        [Required]
        public virtual string FirstName { get; set; }

        [Required]
        public virtual string LastName { get; set; }

        public virtual ICollection<LearningPlusUser> Children { get; set; }

        public virtual ICollection<LearningPlusClassesStudents> ClassesEnrolled { get; set; }

        public virtual ICollection<LearningPlusHomeWork> SubmittedHomeworks { get; set; }

        public virtual ICollection<LearningPlusChat> ChatPosts { get; set; }

        public virtual ICollection<LearningPlusAssessment> Assesments { get; set; }
    }
}
