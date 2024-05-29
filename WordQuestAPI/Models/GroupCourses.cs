using System.ComponentModel.DataAnnotations.Schema; // Pour l'attribut [Column]

namespace WordQuestAPI.Models 
{
    [Table("group_courses")]
    public class GroupCourses {

        [ForeignKey("group_id")]     // Not necessary here. We keep it to be explicit about the foreign key relationship.
        [Column("group_id")]
        public int GroupId { get; set; }

        [ForeignKey("course_id")]     // Not necessary here. We keep it to be explicit about the foreign key relationship.
        [Column("course_id")]
        public int CourseId { get; set; }
    }
}