using LearningPlus.Data.DbRepository.Contract;
using LerningPlus.Web.Services.UsersService.Contract;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace LerningPlus.Web.Services.UsersService
{
    public class UsersService: IUsersService
    {
        private readonly IRepository<IdentityUserRole<string>> userRoleRepo;
        private readonly IRepository<IdentityRole> roleRepo;

        public UsersService(IRepository<IdentityUserRole<string>> userRoleRepo,
            IRepository<IdentityRole> roleRepo)
        {
            this.userRoleRepo = userRoleRepo;
            this.roleRepo = roleRepo;
        }

        public ICollection<string> GetAllUserIdsInRole(string roleName)
        {
            var roleId = roleRepo.All().Where(r => r.Name == roleName).FirstOrDefault().Id;
            var usersIds = userRoleRepo.All().Where(ur => ur.RoleId.Contains(roleId)).Select(ur => ur.UserId).ToList();

            return usersIds;
        }
    }
}
