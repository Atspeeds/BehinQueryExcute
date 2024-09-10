using AccountManagement.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastrure.UserInfra
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.KeyId);
            builder.Property(x => x.FullName).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Mobile).HasMaxLength(12).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();


		

		}
    }
}
