using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestCoreApp.Models.Entities
{
    public class UserSurveyAnswer
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
        public SurveyAnswer Answer { get; set; }
    }
}
