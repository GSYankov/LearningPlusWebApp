using LearningPlus.Web.ViewModels.Chat;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlus.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string text)
        {
            await this.Clients.All.SendAsync("NewMessage", new Message
            {
                User = this.Context.User.Identity.Name,
                Text = text
            });
        }
    }
}
