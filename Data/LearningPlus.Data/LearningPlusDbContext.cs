using LearningPlus.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningPlus.Data
{
    public class LearningPlusDbContext : IdentityDbContext<LearningPlusUser, IdentityRole, string>
    {
        public LearningPlusDbContext(DbContextOptions<LearningPlusDbContext> options)
            : base(options)
        {
        }

    }

}

