using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rishvi.Modules.Linn.Models;
using Rishvi.Modules.Users.Models;

namespace Rishvi.Modules.LabelLog.Linn.Configurations
{
    public class LabelLogsConfiguration : Rishvi.Modules.Core.Data.IEntityTypeConfiguration<LabelLogs>
    {
        public void Map(EntityTypeBuilder<LabelLogs> builder)
        {
            builder.Property(t => t.Id)
                .HasDefaultValueSql("NEWID()");

            builder.HasKey(t => new { t.GenerateLabelId });
        }
    }
}
