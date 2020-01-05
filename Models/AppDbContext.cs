using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCoreApp.Models.Entities;

namespace TestCoreApp.Models
{
    //public class AppUser : IdentityUserContext
    //{        
    //}

    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        /*
        public AppDbContext()
        {

        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }*/

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswers { get; set; }
        public DbSet<UserSurveyAnswer> UserSurveyAnswers { get; set; }
    }
}
