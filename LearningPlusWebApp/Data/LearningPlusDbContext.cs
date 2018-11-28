using System;
using System.Collections.Generic;
using System.Text;
using LearningPlusWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningPlusWebApp.Data
{
    public class LearningPlusDbContext : IdentityDbContext<LearningPlusUser, IdentityRole, string>
    {
        public LearningPlusDbContext(DbContextOptions<LearningPlusDbContext> options)
            : base(options)
        {
        }

    }

}

