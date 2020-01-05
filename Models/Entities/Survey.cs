using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreApp.Models.Entities
{
    public class Survey
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        
        public DateTime AddedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public bool useQuestionDescriptions { get; set; }
        public bool useAnswerDescriptions { get; set; }

        [InverseProperty("BaseSurvey")]
        public virtual List<SurveyQuestion> Questions { get; set; }
    }
}
