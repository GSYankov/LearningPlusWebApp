using LerningPlus.Web.Services.ChatService.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlus.Web.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class ChatController : Controller
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public IActionResult Start()
        {
            var model = this.chatService.GetMessages(GlobalConstants.ChatMessagesToShow);

            return View("Chat", model);
        }
    }
}
