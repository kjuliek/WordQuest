using System.ComponentModel.DataAnnotations.Schema; // Pour l'attribut [Column]

namespace WordQuestAPI.Models 
{
    [Table("group_users")]
    public class GroupUsers {

        [ForeignKey("id")]     // Not necessary here. We keep it to be explicit about the foreign key relationship.
        [Column("user_id")]
        public string UserId { get; set; }

        [ForeignKey("group_id")]     // Not necessary here. We keep it to be explicit about the foreign key relationship.
        [Column("group_id")]
        public int GroupId { get; set; }
    }
}