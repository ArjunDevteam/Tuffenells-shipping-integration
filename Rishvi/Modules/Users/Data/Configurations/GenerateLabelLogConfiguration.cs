using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rishvi.Modules.Linn.Models;
using Rishvi.Modules.Users.Models;

namespace Rishvi.Modules.LabelLog.Linn.Configurations
{
    public class GenerateLabelLogConfiguration : Rishvi.Modules.Core.Data.IEntityTypeConfiguration<GeneratelabelLog>
    {
        public void Map(EntityTypeBuilder<GeneratelabelLog> builder)
        {
            builder.Property(t => t.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
