using System.ComponentModel.DataAnnotations; // Pour l'attribut [Key]
using System.ComponentModel.DataAnnotations.Schema; // Pour l'attribut [Column]
namespace WordQuestAPI.Models
{
    [Table("words")]
    public class Word
    {
        [Key]
        [Column("word_id")] // Correspond au nom de la colonne dans votre base de données
        public int WordId { get; set; }

        [Column("fr_word")] // Correspond au nom de la colonne dans votre base de données
        public string FrWord { get; set; } = string.Empty;

        [Column("en_word")] // Correspond au nom de la colonne dans votre base de données
        public string EnWord { get; set; } = string.Empty;
        /*[InverseProperty("Words")]
        public ICollection<Course>? Courses{ get; set; }*/
    }
}