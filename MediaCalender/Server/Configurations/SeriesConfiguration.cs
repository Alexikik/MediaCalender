using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediaCalender.Shared.ContentTypes;

namespace MediaCalender.Server.Configurations
{
    class SeriesConfiguration : IEntityTypeConfiguration<Series>
    {
        public void Configure(EntityTypeBuilder<Series> builder)
        {
            builder.HasKey(s => s.key);
            builder.Property(s => s.id).IsRequired(true);
            builder.Property(s => s.seriesName).IsRequired(true);
        }
    }
}
