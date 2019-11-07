using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediaCalender.Shared.ContentTypes;

namespace MediaCalender.Server.Configurations
{
    class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
    {
        public void Configure(EntityTypeBuilder<Episode> builder)
        {
            builder.HasKey(s => s.Key);
            builder.Property(s => s.id).IsRequired(true);
        }
    }
}
