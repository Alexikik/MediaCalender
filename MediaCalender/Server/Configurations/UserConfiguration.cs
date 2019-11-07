using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediaCalender.Shared;

namespace MediaCalender.Server.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(s => s.Key);
            builder.Property(s => s.Username).IsRequired(true);
            builder.Property(s => s.Password).IsRequired(true);
        }
    }
}
