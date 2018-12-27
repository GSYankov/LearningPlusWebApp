using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Web.ViewModels.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlus.Web.Hubs
{
    [Authorize(Roles = "Admin, Teacher")]
    public class ChatHub : Hub
    {
        private readonly IRepository<LearningPlusChat> chatRepo;
        private readonly IRepository<LearningPlusUser> userRepo;

        public ChatHub(IRepository<LearningPlusChat> chatRepo, IRepository<LearningPlusUser> userRepo )
        {
            this.chatRepo = chatRepo;
            this.userRepo = userRepo;
        }

        public override Task OnConnectedAsync()
        {
            if (Context.User.IsInRole("Teacher") || Context.User.IsInRole("Admin"))
            {
                var greeting = "Здравейте, тъкмо се присъединявам :)";
                Groups.AddToGroupAsync(Context.ConnectionId, "Teachers").GetAwaiter().GetResult();
                this.SendAsync(greeting).GetAwaiter().GetResult();
                SaveChatPost(greeting);
            }

            return base.OnConnectedAsync();
        }

        public async Task SendAsync(string message)
        {
            await this.Clients.Groups("Teachers").SendAsync("NewMessage", new ChatMessage
            {
                User = this.Context.User.Identity.Name,
                Text = message
            });

            SaveChatPost(message);
        }

        private void SaveChatPost(string message)
        {
            var senderName = this.Context.User.Identity.Name;

            var chatEntry = new LearningPlusChat
            {
                Message = message,
                Sender = this.userRepo.All().SingleOrDefault(u => u.UserName == senderName),
            };

            this.chatRepo.AddAsync(chatEntry).GetAwaiter().GetResult();
            this.chatRepo.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
