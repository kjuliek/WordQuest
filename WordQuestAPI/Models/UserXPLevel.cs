using System.ComponentModel.DataAnnotations.Schema;
namespace WordQuestAPI.Models 
{
    [Table("users")]
    public class UserXPLevel
    {
        [ForeignKey("id")]     // Not necessary here. We keep it to be explicit about the foreign key relationship.
        [Column("user_id")]
        public string UserId { get; set; }

        [Column("user_level")]
        public int UserLevel { get; set; }
        [Column("user_xp")]
        public int UserXP { get; set; }
    }

}