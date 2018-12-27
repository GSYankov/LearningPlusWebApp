using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Web.ViewModels.Chat;
using LerningPlus.Web.Services.ChatService.Contract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LerningPlus.Web.Services.ChatService
{
    public class ChatService : IChatService
    {
        private readonly IRepository<LearningPlusChat> chatRepo;
        private readonly IMapper mapper;

        public ChatService(IRepository<LearningPlusChat> chatRepo, IMapper mapper)
        {
            this.chatRepo = chatRepo;
            this.mapper = mapper;
            MapperConfiguration config = MapperConfiguration();

            this.mapper = new Mapper(config);
        }

        private MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LearningPlusChat, ChatMessage>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.Sender.UserName))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Message));
            });
        }

        public IEnumerable<ChatMessage> GetMessages(int count)
        {
            var messages = this.chatRepo.All().Include(m=>m.Sender).OrderByDescending(m=>m.Time).Take(count).ToList();
            var model = messages.Select(m => this.mapper.Map<ChatMessage>(m));

            return model;
        }
    }
}
