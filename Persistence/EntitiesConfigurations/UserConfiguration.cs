using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurveyBasket.Entities;

namespace SurveyBasket.Persistence.EntitiesConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.OwnsMany(x => x.RefreshTokens).
                ToTable("RefreshTokens").
                WithOwner()
                .HasForeignKey("UserId");

            builder.Property(x=>x.FirstName).HasMaxLength(100);
            builder.Property(x=>x.LastName).HasMaxLength(100);
        }
    }
}
