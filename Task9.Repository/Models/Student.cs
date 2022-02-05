using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task9.SqlRepository.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Введите имя студента")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию студента.")]
        public string LastName { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}
