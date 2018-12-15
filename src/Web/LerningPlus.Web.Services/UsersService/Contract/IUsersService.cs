using System.Collections.Generic;

namespace LerningPlus.Web.Services.UsersService.Contract
{
    public interface IUsersService
    {
        ICollection<string> GetAllUserIdsInRole(string roleName);
    }
}
