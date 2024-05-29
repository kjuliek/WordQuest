using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WordQuestAPI.Models;


namespace WordQuestAPI.Models{
    public class WordQuestContext : DbContext
    {
        public WordQuestContext(DbContextOptions<WordQuestContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Word> Words { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<LearnedWord> LearnedWords { get; set; } = null!;
        public DbSet<CourseWords> CoursesWords { get; set;} = null!;
        public DbSet<GroupCourses> GroupsCourses { get; set; } = null!;
        public DbSet<GroupUsers> GroupsUsers { get; set;} = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Word>().HasKey(w => w.WordId);
            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
            modelBuilder.Entity<Group>().HasKey(g => g.GroupId);
            modelBuilder.Entity<LearnedWord>().HasKey(lw => new { lw.UserId, lw.WordId });
            modelBuilder.Entity<CourseWords>().HasKey(cw => new { cw.CourseId, cw.WordId });
            modelBuilder.Entity<GroupCourses>().HasKey(gc => new { gc.GroupId, gc.CourseId});
            modelBuilder.Entity<GroupUsers>().HasKey(gu => new { gu.GroupId, gu.UserId});

            // Configure relationships
            modelBuilder.Entity<LearnedWord>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(lw => lw.UserId)
                .IsRequired(); 

            modelBuilder.Entity<LearnedWord>()
                .HasOne<Word>()
                .WithMany()
                .HasForeignKey(lw => lw.WordId)
                .IsRequired();

            modelBuilder.Entity<CourseWords>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(cw => cw.CourseId)
                .IsRequired(); 

            modelBuilder.Entity<CourseWords>()
                .HasOne<Word>()
                .WithMany()
                .HasForeignKey(cw => cw.WordId)
                .IsRequired();

            modelBuilder.Entity<GroupCourses>()
                .HasOne<Group>()
                .WithMany()
                .HasForeignKey(gc => gc.GroupId)
                .IsRequired(); 

            modelBuilder.Entity<GroupCourses>()
                .HasOne<Course>()
                .WithMany()
                .HasForeignKey(gc => gc.CourseId)
                .IsRequired();

            modelBuilder.Entity<GroupUsers>()
                .HasOne<Group>()
                .WithMany()
                .HasForeignKey(gu => gu.GroupId)
                .IsRequired(); 

            modelBuilder.Entity<GroupUsers>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(gu => gu.UserId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }    
}