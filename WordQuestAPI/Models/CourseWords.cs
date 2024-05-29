using System.ComponentModel.DataAnnotations.Schema; // Pour l'attribut [Column]

namespace WordQuestAPI.Models 
{
    [Table("course_words")]
    public class CourseWords {

        [ForeignKey("course_id")]     // Not necessary here. We keep it to be explicit about the foreign key relationship.
        [Column("course_id")]
        public int CourseId { get; set; }

        [ForeignKey("word_id")]     // Not necessary here. We keep it to be explicit about the foreign key relationship.
        [Column("word_id")]
        public int WordId { get; set; }
    }
}