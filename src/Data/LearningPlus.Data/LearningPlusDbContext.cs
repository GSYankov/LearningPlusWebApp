using LearningPlus.Models;
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

        public virtual DbSet<LearningPlusNews> News { get; set; }

        public virtual DbSet<LearningPlusNewsTargetRole> NewsTargetRole { get; set; }

        public virtual DbSet<LearningPlusClass> Classes { get; set; }

        public virtual DbSet<LearningPlusStudentsClasses> StudentsClasses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LearningPlusNewsTargetRole>(e => e.HasKey(n => new { n.NewsId, n.TargetRole }));
            modelBuilder.Entity<LearningPlusStudentsClasses>(e => e.HasKey(n => new { n.StudentId, n.ClassId }));
        }

    }

}

