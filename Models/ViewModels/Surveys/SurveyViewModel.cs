using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreApp.Models.ViewModels.Surveys
{
    public class SurveyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public string UserStatus { get; set; }

        [Display(Name = "Use Question Descriptions")]
        public bool useQuestionDescriptions { get; set; }
        [Display(Name = "Use Answer Descriptions")]
        public bool useAnswerDescriptions { get; set; }

        public virtual List<SurveyQuestionViewModel> Questions { get; set; }
    }

    public class SurveyQuestionViewModel
    {
        public int Id { get; set; }
        public int Sort { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<SurveyAnswerViewModel> Answers { get; set; }
    }

    public class SurveyAnswerViewModel
    {
        public int Id { get; set; }
        public int Sort { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Checked { get; set; }
    }
 }
