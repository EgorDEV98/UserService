using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Data.Entities;

namespace UserService.Data.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Login).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(256);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
        builder.Property(x => x.City).HasMaxLength(256);
        builder.Property(x => x.CreatedDate).IsRequired();
        builder.Property(x => x.LastUpdate).IsRequired();
    }
}