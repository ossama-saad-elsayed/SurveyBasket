using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;
using SurveyBasket.Entities;
using SurveyBasket.Persistence.EntitiesConfigurations;
using System.Reflection;
namespace SurveyBasket.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):DbContext(options)
    {
      public  DbSet<Poll> Polls {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
            base.OnModelCreating(modelBuilder);
        }
    }
}
