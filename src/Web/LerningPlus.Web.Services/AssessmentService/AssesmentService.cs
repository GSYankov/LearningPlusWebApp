using AutoMapper;
using LearningPlus.Data.DbRepository.Contract;
using LearningPlus.Models;
using LearningPlus.Models.Enums;
using LearningPlus.Web.ViewModels.Assessment;
using LerningPlus.Web.Services.AssessmentService.Contract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LerningPlus.Web.Services.AssessmentService
{
    public class AssesmentService : IAssessmentService
    {
        private readonly UserManager<LearningPlusUser> userManager;
        private readonly IMapper mapper;
        private readonly IRepository<LearningPlusAssessment> assessmentRepo;

        public AssesmentService(UserManager<LearningPlusUser> userManager, 
            IMapper mapper, 
            IRepository<LearningPlusAssessment> assessmentRepo)
        {
            this.userManager = userManager;
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
            var child = userManager.FindByIdAsync(id).GetAwaiter().GetResult();
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
                cfg.CreateMap<LearningPlusAssessment, AssessmentViewModel>();
                cfg.CreateMap<DoAssessmentViewModel, LearningPlusAssessment>()
                .ForMember(dest => dest.Child,opt=> opt.MapFrom(src => this.userManager.FindByIdAsync(src.ChildId).GetAwaiter().GetResult()));
            });
        }
    }
}

