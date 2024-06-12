using System.ComponentModel.DataAnnotations;
namespace WordQuestAPI.Models
{
    public class Word
    {
        [Key]
        public int WordId { get; set; }
        public string FrWord { get; set; } = string.Empty;
        public string EnWord { get; set; } = string.Empty;
    }
}