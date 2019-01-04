using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearningPlus.Data
{
    class LearningPlusContextFactory : IDesignTimeDbContextFactory<LearningPlusDbContext>
    {
        public LearningPlusDbContext CreateDbContext(string[] args)
        {
            var path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.LastIndexOf(@"\src\")) + @"\src\Web\LearningPlusWebApp\appsettings.json";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path, optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<LearningPlusDbContext>();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            builder.UseSqlServer(connectionString);

            // Stop client query evaluation
            builder.ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning));

            return new LearningPlusDbContext(builder.Options);
        }
    }
}
