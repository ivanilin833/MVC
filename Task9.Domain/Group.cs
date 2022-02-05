using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task9.Domain
{
    public class Group
    {
        public int GroupId { get; set; }
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Введите имя группы.")]
        public string Name { get; set; }
        public Course Courses { get; set; }
        public List<Student> Students { get; set; }
    }
}
