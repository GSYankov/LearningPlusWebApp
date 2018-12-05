using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearningPlusWebApp.Areas.Teacher.ViewModels
{
    public class UserApprovalViewModel
    {
        public string ParentId { get; set; }

        public string ParentName { get; set; }

        public string ChildId { get; set; }

        public string ChildName { get; set; }
    }
}
