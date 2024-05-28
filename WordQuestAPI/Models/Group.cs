using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordQuestAPI.Models {
    [Table("Groups")]
    public class Group
    {
        [Key]
        [Column("group_id")]
        public int GroupId { get; set; }
        [Column("group_name")]
        public string GroupName { get; set; } = string.Empty;
        [ForeignKey("AdminId")]
        [Column("admin_id")]
        public int AdminId { get; set; }
        [InverseProperty("AdministeredGroups")]
        public required User GroupAdmin {get; set; }
        [InverseProperty("Groups")]
        public ICollection<User> Members { get; set; } = new List<User>();
        [InverseProperty("Groups")]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}