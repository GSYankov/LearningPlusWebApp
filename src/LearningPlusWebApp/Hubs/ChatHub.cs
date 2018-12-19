using LearningPlus.Web.ViewModels.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace LearningPlus.Web.Hubs
{
    [Authorize(Roles = "Admin, Teacher")]
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            if (Context.User.IsInRole("Teacher") || Context.User.IsInRole("Admin"))
            {
                Groups.AddToGroupAsync(Context.ConnectionId, "Teachers").GetAwaiter().GetResult();
                this.SendAsync("Здравейте, тъкмо се присъединявам :)").GetAwaiter().GetResult();
            }

            return base.OnConnectedAsync();
        }

        public async Task SendAsync(string text)
        {
            await this.Clients.Groups("Teachers").SendAsync("NewMessage", new Message
            {
                User = this.Context.User.Identity.Name,
                Text = text
            });
        }
    }
}
