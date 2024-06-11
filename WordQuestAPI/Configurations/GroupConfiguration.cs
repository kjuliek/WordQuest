using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordQuestAPI.Models;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("Groups");

        builder.Property(g => g.GroupId).HasColumnName("group_id");
        builder.Property(g => g.GroupName).HasColumnName("group_name");
        builder.Property(g => g.AdminId).HasColumnName("admin_id");
    }
}

