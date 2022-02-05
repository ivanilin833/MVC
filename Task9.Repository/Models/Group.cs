using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task9.SqlRepository.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Введите название группы.")]
        public string Name { get; set; }
        [ForeignKey("CourseId")]
        public Course Courses { get; set; }
        public List<Student> Students { get; set; }
    }
}
