using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rishvi.Modules.Linn.Models;
using Rishvi.Modules.Users.Models;

namespace Rishvi.Modules.Users.Linn.Configurations
{
    public class UserConfiguration : Rishvi.Modules.Core.Data.IEntityTypeConfiguration<linnUser>
    {
        public void Map(EntityTypeBuilder<linnUser> builder)
        {
            builder.Property(t => t.Id)
                .HasDefaultValueSql("NEWID()");
        }
    }
}
