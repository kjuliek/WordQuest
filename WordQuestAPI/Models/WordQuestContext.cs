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
        public DbSet<LearnedWord> LearnedWords { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure primary keys
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Word>().HasKey(w => w.WordId);
            modelBuilder.Entity<LearnedWord>().HasKey(lw => new { lw.UserId, lw.WordId });
            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
            modelBuilder.Entity<Group>().HasKey(g => g.GroupId);

            // Configure relationships
            modelBuilder.Entity<LearnedWord>()
                .HasOne<User>()
                .WithMany(u => u.LearnedWords)
                .HasForeignKey(lw => lw.UserId)
                .IsRequired(); 

            modelBuilder.Entity<LearnedWord>()
                .HasOne<Word>()
                .WithMany()
                .HasForeignKey(lw => lw.WordId)
                .IsRequired(); 

            base.OnModelCreating(modelBuilder);
        }
    }    
}