using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rishvi.Modules.Linn.Models;
using Rishvi.Modules.Users.Models;

namespace Rishvi.Modules.Users.Linn.Configurations
{
    public class GenerateLabelCountConfiguration : Rishvi.Modules.Core.Data.IEntityTypeConfiguration<GenerateLabelCount>
    {
        public void Map(EntityTypeBuilder<GenerateLabelCount> builder)
        {
            builder.Property(t => t.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
