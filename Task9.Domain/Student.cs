using System.ComponentModel.DataAnnotations;

namespace Task9.Domain
{
    public class Student
    {
        public int StudentId { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Введите имя студента.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию студента.")]
        public string LastName { get; set; }
        public Group Group { get; set; }
    }
}
