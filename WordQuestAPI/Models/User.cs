using System.ComponentModel.DataAnnotations; // Pour l'attribut [Key]
using System.ComponentModel.DataAnnotations.Schema; // Pour l'attribut [Column]

namespace WordQuestAPI.Models 
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("user_name")]
        public string UserName { get; set; } = string.Empty;
        [Column("user_password")]
        public string UserPassword { get; set; } = string.Empty;
        [Column("user_email")]
        public string UserEmail { get; set; } = string.Empty;
        [Column("user_level")]
        public int UserLevel { get; set; }
        [Column("user_xp")]
        public int UserXP { get; set; }
        public ICollection<LearnedWord> LearnedWords  { get; set; } = new List<LearnedWord>();

        [InverseProperty("Members")]
        public ICollection<Group> Groups { get; set; } = new List<Group>();
        [InverseProperty("GroupAdmin")]
        public ICollection<Group> AdministeredGroups { get; set; } = new List<Group>();
        [InverseProperty("CourseCreator")]
        public ICollection<Course> CreatedCourses{ get; set; }= new List<Course>();
    }
}