using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace WordQuestAPI.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string CourseDescription { get; set; } = string.Empty;
        [ForeignKey("CreatorId")]
        public string CreatorId { get; set;}
        public int CourseLevel { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}