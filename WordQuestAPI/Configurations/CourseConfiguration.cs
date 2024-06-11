using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WordQuestAPI.Models;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");

        builder.Property(c => c.CourseId).HasColumnName("course_id");
        builder.Property(c => c.CourseName).HasColumnName("course_name");
        builder.Property(c => c.CourseDescription).HasColumnName("course_description");
        builder.Property(c => c.CreatorId).HasColumnName("creator_id");
        builder.Property(c => c.CourseLevel).HasColumnName("course_level");
        builder.Property(c => c.CreationDate).HasColumnName("creation_date");
    }
}