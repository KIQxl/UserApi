using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(u => u.Phone)
                .HasColumnType("varchar(11)")
                .IsRequired();

            builder.Property(u => u.UserName)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(u => u.Salt)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(u => u.Role)
                .HasColumnType("varchar(25)")
                .IsRequired();

            builder.Property(u => u.CreationDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(u => u.ModificationDate)
                .HasColumnType("datetime");

            builder.HasIndex(u => new { u.Email, u.UserName })
                .IsUnique();
        }
    }
}
