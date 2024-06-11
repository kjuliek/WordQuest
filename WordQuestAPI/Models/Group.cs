using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordQuestAPI.Models {
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; } = string.Empty;

        [ForeignKey("AdminId")]
        public string AdminId { get; set; }
    }
}