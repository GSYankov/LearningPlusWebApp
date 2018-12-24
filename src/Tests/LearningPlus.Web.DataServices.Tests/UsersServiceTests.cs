using LearningPlus.Data.DbRepository.Contract;
using LerningPlus.Web.Services.UsersService;
using Microsoft.AspNetCore.Identity;
using Moq;
using Newtonsoft.Json;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LearningPlus.Web.DataServices.Tests
{
    public class UsersServiceTests
    {
        [Fact]
        public void GetAllUserIdsInRoleReturnsCorrectIds()
        {
            var userRoleRepo = new Mock<IRepository<IdentityUserRole<string>>>();
            userRoleRepo.Setup(r => r.All()).Returns(new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>{ UserId = "1", RoleId = "1"},
                new IdentityUserRole<string>{ UserId = "1", RoleId = "2"},
                new IdentityUserRole<string>{ UserId = "1", RoleId = "3"},
                new IdentityUserRole<string>{ UserId = "2", RoleId = "1"},
            }.AsQueryable());

            var roleRepo = new Mock<IRepository<IdentityRole>>();
            roleRepo.Setup(r => r.All()).Returns(new List<IdentityRole>
            {
              new IdentityRole{ Name = "a", Id = "1"},
              new IdentityRole{ Name = "b", Id = "2"},
              new IdentityRole{ Name = "c", Id = "3"},
            }.AsQueryable());

            var expextedResult = new List<string> { "1", "2" };
            var expectedJson = JsonConvert.SerializeObject(expextedResult);

            var usersService = new UsersService(userRoleRepo.Object, roleRepo.Object);


            var userIds = usersService.GetAllUserIdsInRole("a");
            var userIdsJson = JsonConvert.SerializeObject(userIds);

            Assert.Equal(expectedJson, userIdsJson);
        }
    }
}
