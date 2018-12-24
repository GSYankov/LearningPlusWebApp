using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LearningPlus.Web.DataServices.Tests
{
   public static class TestsPrerequisites
    {
        public static IConfiguration GetConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            // Duplicate here any configuration sources you use.
            var path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.LastIndexOf(@"\src\")) + @"\src\Web\LearningPlusWebApp\appsettings.json";
            configurationBuilder.AddJsonFile(path);
            IConfiguration configuration = configurationBuilder.Build();

            return configuration;
        }
    }
}
