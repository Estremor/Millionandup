using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Millionandup.PropertyManagement.Domain.Entities;

namespace Millionandup.PropertyManagement.Infrastructure.DataPersistence.EntitiesConfig
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(e => e.IdUser);
            builder.Property(e => e.IdUser).ValueGeneratedOnAdd();
            builder.Property(e => e.Password).IsRequired();
            builder.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
        }
    }
}
