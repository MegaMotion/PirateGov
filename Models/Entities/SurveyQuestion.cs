using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreApp.Models.Entities
{
    public class SurveyQuestion
    {
        [Key]
        public int Id { get; set; }
        public int Sort { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [InverseProperty("Questions")]
        public Survey BaseSurvey { get; set; }

        [InverseProperty("Question")]
        public virtual List<SurveyAnswer> Answers { get; set; }
    }
}
