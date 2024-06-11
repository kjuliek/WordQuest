using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordQuestAPI.Models;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("AspNetUsers");

        builder.Property(u => u.Id).HasColumnName("Id");
        builder.Property(u => u.UserName).HasColumnName("UserName");
        builder.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
        builder.Property(u => u.Email).HasColumnName("Email");
        builder.Property(u => u.PhoneNumber).HasColumnName("PhoneNumber");
    }
}

