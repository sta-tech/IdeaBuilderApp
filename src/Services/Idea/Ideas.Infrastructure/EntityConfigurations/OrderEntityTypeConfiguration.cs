using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Ideas.Domain.AggregatesModel;

namespace Ideas.Infrastructure.EntityConfigurations
{
    internal class IdeaEntityTypeConfiguration : IEntityTypeConfiguration<Idea>
    {
        public void Configure(EntityTypeBuilder<Idea> ideaConfiguration)
        {
            ideaConfiguration.ToTable("ideas", IdeasContext.DEFAULT_SCHEMA);

            ideaConfiguration.HasKey(o => o.Id);

            ideaConfiguration.Ignore(b => b.DomainEvents);

            ideaConfiguration.Property(e => e.CreatedAt)
                .IsRequired(true);

            ideaConfiguration.Property(e => e.Title)
                .HasMaxLength(300)
                .IsRequired(true);

            ideaConfiguration.Property(e => e.Description)
                .IsRequired(false);
        }
    }
}