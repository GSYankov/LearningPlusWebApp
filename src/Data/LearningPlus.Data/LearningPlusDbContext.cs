using LearningPlus.Models;
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

        public virtual DbSet<LearningPlusClassesStudents> ClassesStudents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LearningPlusNewsTargetRole>(e => e.HasKey(n => new { n.NewsId, n.TargetRole }));
            modelBuilder.Entity<LearningPlusClassesStudents>(e => e.HasKey(cs => new { cs.ClassId, cs.StudentId }));
        }

    }

}

