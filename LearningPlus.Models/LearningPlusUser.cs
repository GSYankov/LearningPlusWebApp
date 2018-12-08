using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlus.Web.Models
{
    public class LearningPlusUser : IdentityUser
    {
        public LearningPlusUser()
        {
            this.Children = new HashSet<LearningPlusUser>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public virtual ICollection<LearningPlusUser> Children { get; set; }
    }
}
