using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediaCalender.Shared.ContentTypes;

namespace MediaCalender.Server.Configurations
{
    class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
        builder.HasKey(m => m.Key);
        builder.Property(s => s.imdbID).IsRequired(true);
        builder.Property(s => s.Title).IsRequired(true);
        }
    }
}