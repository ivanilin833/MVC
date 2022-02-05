using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Task9.SqlRepository.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Введите название курса.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Введите описание курса.")]
        public string Description { get; set; }
        public ICollection<Group> Group { get; set; }
    }
}
