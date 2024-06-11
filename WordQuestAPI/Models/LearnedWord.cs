using System.ComponentModel.DataAnnotations.Schema;

namespace WordQuestAPI.Models 
{
    [Table("learned_words")]
    public class LearnedWord {

        [ForeignKey("id")]     // Not necessary here. We keep it to be explicit about the foreign key relationship.
        [Column("user_id")]
        public string UserId { get; set; }

        [ForeignKey("word_id")]     // Not necessary here. We keep it to be explicit about the foreign key relationship.
        [Column("word_id")]
        public int WordId { get; set; }
        [Column("learning_stage")]
        public int LearningStage { get; set; }
    }
}