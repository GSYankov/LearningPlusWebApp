using LearningPlus.Data;
using LearningPlus.Data.DbRepository;
using LearningPlus.Models;
using LerningPlus.Web.Services.ChatService;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LearningPlus.Web.DataServices.Tests
{
    public class ChatServiceTests
    {
        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void GetMessagesReturnsCorrectCollection(int value)
        {
            var mockMessage = new Mock<LearningPlusChat>();
            mockMessage.Setup(m => m.Message).Returns("NewMessage");

            var mock2Message = new Mock<LearningPlusChat>();
            mock2Message.Setup(m => m.Message).Returns("NewMessage");

            var options = new DbContextOptionsBuilder<LearningPlusDbContext>()
            .UseInMemoryDatabase(databaseName: "FakeDelete_News_Database") // Give a Unique name to the DB
            .Options;
            var dbContext = new LearningPlusDbContext(options);
            dbContext.Add(mockMessage.Object);
            dbContext.Add(mock2Message.Object);
            dbContext.SaveChanges();
            var chatRepo = new DbRepository<LearningPlusChat>(dbContext);
            var chatService = new ChatService(chatRepo, null);

            var messages = chatService.GetMessages(value);

            messages.Count().ShouldBe(2);
        }
    }
}
