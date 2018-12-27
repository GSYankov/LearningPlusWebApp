using LearningPlus.Data;
using LearningPlus.Data.DbRepository;
using LearningPlus.Models;
using LerningPlus.Web.Services.NewsService;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
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
                 .UseInMemoryDatabase(databaseName: "GetById_News_Database") // Give a Unique name to the DB
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

        [Fact]
        public void GetAdminNewsReturnsCorrectCollection()
        {
            var mockNews = new Mock<LearningPlusNews>();
            mockNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            var mockSndNews = new Mock<LearningPlusNews>();
            mockSndNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddSeconds(5));
            var mockOldNews = new Mock<LearningPlusNews>();
            mockOldNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(-1));

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "GetAdminNews_News_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.News.Add(mockNews.Object);
            dbContext.News.Add(mockSndNews.Object);
            dbContext.News.Add(mockOldNews.Object);
            dbContext.SaveChanges();
            var newsRepo = new DbRepository<LearningPlusNews>(dbContext);
            var lpNewsService = new LearningPlusNewsService(null, newsRepo);

            var news = lpNewsService.GetAdminNews();

            news.Count.ShouldBe(2);
        }

        [Fact]
        public void GetTeacherNewsReturnsCorrectCollection()
        {
            var mockNews = new Mock<LearningPlusNews>();
            mockNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            var mockAdminNews = new Mock<LearningPlusNews>();
            mockAdminNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddSeconds(5));
            mockAdminNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Admin }});
            var mockTeacherNews = new Mock<LearningPlusNews>();
            mockTeacherNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            mockTeacherNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Teacher }});
            var mockOldNews = new Mock<LearningPlusNews>();
            mockOldNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(-1));

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "GetTeacherNews_News_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.News.Add(mockAdminNews.Object);
            dbContext.News.Add(mockTeacherNews.Object);
            dbContext.News.Add(mockNews.Object);
            dbContext.News.Add(mockOldNews.Object);
            dbContext.SaveChanges();
            var newsRepo = new DbRepository<LearningPlusNews>(dbContext);
            var lpNewsService = new LearningPlusNewsService(null, newsRepo);

            var news = lpNewsService.GetTeacherNews();

            news.Count.ShouldBe(1);
        }

        [Fact]
        public void GetParentChildNewsReturnsCorrectCollection()
        {
            var mockNews = new Mock<LearningPlusNews>();
            mockNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            var mockAdminNews = new Mock<LearningPlusNews>();
            mockAdminNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddSeconds(5));
            mockAdminNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Admin }});
            var mockTeacherNews = new Mock<LearningPlusNews>();
            mockTeacherNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            mockTeacherNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Teacher }});
            var mockParentNews = new Mock<LearningPlusNews>();
            mockParentNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            mockParentNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Parent }});
            var mockChildNews = new Mock<LearningPlusNews>();
            mockChildNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            mockChildNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Child }});
            var mockChildSndNews = new Mock<LearningPlusNews>();
            mockChildSndNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(-1));
            mockChildSndNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Child }});
            var mockOldNews = new Mock<LearningPlusNews>();
            mockOldNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(-1));

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "GetParentChildNews_News_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.News.Add(mockAdminNews.Object);
            dbContext.News.Add(mockTeacherNews.Object);
            dbContext.News.Add(mockNews.Object);
            dbContext.News.Add(mockOldNews.Object);
            dbContext.News.Add(mockParentNews.Object);
            dbContext.News.Add(mockChildNews.Object);
            dbContext.News.Add(mockOldNews.Object);
            dbContext.SaveChanges();
            var newsRepo = new DbRepository<LearningPlusNews>(dbContext);
            var lpNewsService = new LearningPlusNewsService(null, newsRepo);

            var news = lpNewsService.GetParentChildNews();

            news.Count.ShouldBe(2);
        }

        [Fact]
        public void GetArchivedNewsReturnsCorrectCollection()
        {
            var mockNews = new Mock<LearningPlusNews>();
            mockNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            var mockAdminNews = new Mock<LearningPlusNews>();
            mockAdminNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddSeconds(5));
            mockAdminNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Admin }});
            var mockTeacherNews = new Mock<LearningPlusNews>();
            mockTeacherNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            mockTeacherNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Teacher }});
            var mockParentNews = new Mock<LearningPlusNews>();
            mockParentNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            mockParentNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Parent }});
            var mockChildNews = new Mock<LearningPlusNews>();
            mockChildNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(2));
            mockChildNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Child }});
            var mockChildSndNews = new Mock<LearningPlusNews>();
            mockChildSndNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(-1));
            mockChildSndNews.Setup(n => n.TargetRoles).Returns(new List<LearningPlusNewsTargetRole> {
                                                                new LearningPlusNewsTargetRole {
                                                                      TargetRole = Models.Enums.UserRoles.Child }});
            var mockOldNews = new Mock<LearningPlusNews>();
            mockOldNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow.AddDays(-1));

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "GetArchivedNews_News_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.News.Add(mockAdminNews.Object);
            dbContext.News.Add(mockTeacherNews.Object);
            dbContext.News.Add(mockNews.Object);
            dbContext.News.Add(mockOldNews.Object);
            dbContext.News.Add(mockParentNews.Object);
            dbContext.News.Add(mockChildNews.Object);
            dbContext.News.Add(mockChildSndNews.Object);
            dbContext.News.Add(mockOldNews.Object);
            dbContext.SaveChanges();
            var newsRepo = new DbRepository<LearningPlusNews>(dbContext);
            var lpNewsService = new LearningPlusNewsService(null, newsRepo);

            var news = lpNewsService.GetArchivedNews();

            news.Count.ShouldBe(2);
        }

        [Fact]
        public void FakeDeleteReturnsCorrectItem()
        {
            var mockNews = new Mock<LearningPlusNews>();
            mockNews.Setup(n => n.ExpiresOn).Returns(DateTime.UtcNow);

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
                 .UseInMemoryDatabase(databaseName: "FakeDelete_News_Database") // Give a Unique name to the DB
                 .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.News.Add(mockNews.Object);

            dbContext.SaveChanges();
            var newsRepo = new DbRepository<LearningPlusNews>(dbContext);
            var lpNewsService = new LearningPlusNewsService(null, newsRepo);
            var id = newsRepo.All().FirstOrDefaultAsync().GetAwaiter().GetResult().Id.ToString();

            var news = lpNewsService.FakeDelete(id);

            DateTime.UtcNow.AddDays(-1).ShouldBeLessThanOrEqualTo(news.ExpiresOn);
        }
    }
}
