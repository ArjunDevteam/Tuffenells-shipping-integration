using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rishvi.Modules.Core.Data;
using Rishvi.Modules.Users.Models;

namespace Rishvi.Modules.Users.Data.Configurations
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Map(EntityTypeBuilder<User> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(500);
        }
    }
}
