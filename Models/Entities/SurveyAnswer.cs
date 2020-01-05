using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestCoreApp.Models.Entities
{
    public class SurveyAnswer
    {
        [Key]
        public int Id { get; set; }
        public int Sort { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        
        public Survey survey { get; set; }

        [InverseProperty("Answers")]
        public SurveyQuestion Question { get; set; }
    }
}
