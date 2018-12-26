using LearningPlus.Data;
using LearningPlus.Data.DbRepository;
using LearningPlus.Models;
using LerningPlus.Web.Services.NewsService;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LearningPlus.Web.DataServices.Tests
{
   public class NewsServiceTests
    {
        [Fact]
        public void GetByIdReturnsCorrectNews()
        {
            var mockNews = new Mock<LearningPlusNews>();

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "Teacher_Classes_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.News.Add(mockNews.Object);
            dbContext.SaveChanges();
            var newsRepo = new DbRepository<LearningPlusNews>(dbContext);
            var newsId = newsRepo.All().FirstOrDefaultAsync().GetAwaiter().GetResult().Id.ToString();
            var lpNewsService = new LearningPlusNewsService(null, newsRepo);

            var newsById = lpNewsService.GetById(newsId);

            newsById.CountAsync().GetAwaiter().GetResult().ShouldBe(1);
        }
    }
}
