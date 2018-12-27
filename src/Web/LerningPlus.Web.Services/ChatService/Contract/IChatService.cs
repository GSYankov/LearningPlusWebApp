using LearningPlus.Web.ViewModels.Chat;
using System;
using System.Collections.Generic;
using System.Text;

namespace LerningPlus.Web.Services.ChatService.Contract
{
    public interface IChatService
    {
        IEnumerable<ChatMessage> GetMessages(int count);
    }
}
