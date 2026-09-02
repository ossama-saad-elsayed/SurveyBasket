using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;
using SurveyBasket.Entities;
using SurveyBasket.Persistence.EntitiesConfigurations;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
namespace SurveyBasket.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : IdentityDbContext<User>(options)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public  DbSet<Poll> Polls {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());  
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditableEntity>();
            var  CurrentUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            foreach (var entityentry in entries)
            {
                if (entityentry.State == EntityState.Added)
                {
                    entityentry.Property(x => x.CreatedById).CurrentValue = CurrentUserId;
                } else if (entityentry.State == EntityState.Modified)
                {
                    entityentry.Property(x => x.UpdatedById).CurrentValue = CurrentUserId;
                    entityentry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                }

            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
