using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace WordQuestAPI.Models
{
    [Table("courses")]
    public class Course
    {
        [Key]
        [Column("course_id")]
        public int CourseId { get; set; }
        [Column("course_name")]
        public string CourseName { get; set; } = string.Empty;
        [Column("course_description")]
        public string CourseDescription { get; set; } = string.Empty;
        [ForeignKey("CreatorId")]
        [Column("creator_id")]
        public int CreatorId { get; set;}
        //[InverseProperty("CreatedCourses")]
        //public required User CourseCreator { get; set; }
        [Column("course_level")]
        public int CourseLevel { get; set; }
        [Column("creation_date")]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        
        //[InverseProperty("Courses")]
        
        //public ICollection<Word> Words  { get; set; } = new List<Word>();
        /*
        [InverseProperty("Courses")]
        public ICollection<Group>? Groups { get; set; }*/
    }
}