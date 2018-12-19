using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual ICollection<LearningPlusUser> Children { get; set; }

        public virtual ICollection<LearningPlusClassesStudents> ClassesEnrolled { get; set; }

    }
}
