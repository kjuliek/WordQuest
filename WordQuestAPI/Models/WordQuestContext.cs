using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Identity;
using Microsoft.AspNetCore.Identity;

using Newtonsoft.Json;
using WordQuestAPI.Models;


namespace WordQuestAPI.Models{
    public class WordQuestContext : IdentityDbContext<User>
    {
        public WordQuestContext(DbContextOptions<WordQuestContext> options)
            : base(options) {
                // Database initialization logic
                InitializeDatabase();
            }

        public new DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<LearnedWord> LearnedWords { get; set; } = null!;
        public DbSet<CourseWords> CoursesWords { get; set;} = null!;
        public DbSet<GroupCourses> GroupsCourses { get; set; } = null!;
        public DbSet<GroupUsers> GroupsUsers { get; set;} = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Appliquer la configuration de l'entité User
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new WordConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            
            // Configure primary keys
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Word>().HasKey(w => w.WordId);
            modelBuilder.Entity<Course>().HasKey(c => c.CourseId);
            modelBuilder.Entity<Group>().HasKey(g => g.GroupId);
            modelBuilder.Entity<UserXPLevel>().HasKey(ul => ul.UserId);
            modelBuilder.Entity<LearnedWord>().HasKey(lw => new { lw.UserId, lw.WordId });
            modelBuilder.Entity<CourseWords>().HasKey(cw => new { cw.CourseId, cw.WordId });
            modelBuilder.Entity<GroupCourses>().HasKey(gc => new { gc.GroupId, gc.CourseId});
            modelBuilder.Entity<GroupUsers>().HasKey(gu => new { gu.GroupId, gu.UserId});

            // Configure relationships
            modelBuilder.Entity<UserXPLevel>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(ul => ul.UserId)
                .IsRequired(); 
            
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

        private void InitializeDatabase()
        {
            if (Words.CountAsync().Result == 0)
            {
                // Load data from JSON file
                var jsonData = File.ReadAllText("Data/words.json");
                var words = JsonConvert.DeserializeObject<List<Word>>(jsonData);

                // Add words to the database
                Words.AddRange(words);
                SaveChanges();
            }
        }
    }    
}