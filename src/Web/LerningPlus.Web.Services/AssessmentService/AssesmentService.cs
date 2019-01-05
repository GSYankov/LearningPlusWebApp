using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Models.Enums;
using LearningPlus.Web.ViewModels.Assessment;
using LerningPlus.Web.Services.AssessmentService.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LerningPlus.Web.Services.AssessmentService
{
    public class AssesmentService : IAssessmentService
    {
        private readonly IRepository<LearningPlusUser> userRepo;
        private readonly IMapper mapper;
        private readonly IRepository<LearningPlusAssessment> assessmentRepo;

        public AssesmentService(IRepository<LearningPlusUser> userRepo,
            IMapper mapper,
            IRepository<LearningPlusAssessment> assessmentRepo)
        {
            this.userRepo = userRepo;
            this.mapper = mapper;
            this.assessmentRepo = assessmentRepo;
            MapperConfiguration config = MapperConfiguration();
            this.mapper = new Mapper(config);
        }

        public LearningPlusAssessment CreateAssessment(DoAssessmentViewModel model)
        {
            var assessment = this.mapper.Map<LearningPlusAssessment>(model);
            this.assessmentRepo.AddAsync(assessment).GetAwaiter().GetResult();
            this.assessmentRepo.SaveChangesAsync().GetAwaiter().GetResult();

            return assessment;
        }

        public MarkbookViewModel GetMarkbookById(string id)
        {
            var child = userRepo.All().Include(u => u.Assesments).SingleOrDefault(u => u.Id == id);
            var assessments = child.Assesments?.Select(a => this.mapper.Map<AssessmentViewModel>(a)).ToList();
            var model = new MarkbookViewModel()
            {
                Assessments = assessments,
                ChildName = $"{child.FirstName} {child.LastName}",
                ChildId = id,
            };

            return model;
        }

        private MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LearningPlusAssessment, AssessmentViewModel>()
                .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Course.ToString()));
                cfg.CreateMap<DoAssessmentViewModel, LearningPlusAssessment>()
                .ForMember(dest => dest.Child, opt => opt.MapFrom(src => this.userRepo.All().SingleOrDefault(u => u.Id == src.ChildId)))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => Enum.Parse<Disciplines>(src.Discipline)));
            });
        }
    }
}

