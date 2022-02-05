using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task9.Domain;

namespace Task9.SqlRepository
{
    public class DbOdjects
    {
        public static async Task Initial(AppDBContext content)
        {
            if (!content.Courses.Any())
            {
                content.Courses.AddRange(addCours);
            }

            if (!content.Groups.Any())
            {
                content.Groups.AddRange(addGroup);
            }

            if (!content.Students.Any())
            {
                content.Students.AddRange(addStudent);
            }

            await content.SaveChangesAsync();
        }

        private static List<Course> _courses;

        public static List<Course> addCours
        {
            get
            {
                if (_courses == null)
                {
                    var list = new Course[]
                    {
                        new Course{ Name="Math", Description="Course for Math" },
                        new Course{ Name="Biologic", Description="Course for Biologic" },
                        new Course{ Name="Chimia", Description="Course for Chimia" }
                    };

                    _courses = new List<Course>();

                    foreach (Course cours in list)
                    {
                        _courses.Add(cours);
                    }
                }
                return _courses;
            }
        }

        private static List<Group> _group;

        public static List<Group> addGroup
        {
            get
            {
                if (_group == null)
                {
                    var list = new Group[]
                    {
                        new Group{ CourseId=1,  Name="A1" },
                        new Group{ CourseId=1,  Name="A2" },
                        new Group{ CourseId=2,  Name="B1" },
                        new Group{ CourseId=2,  Name="B2" },
                        new Group{ CourseId=3,  Name="C1" },
                        new Group{ CourseId=3,  Name="C2" },
                    };

                    _group = new List<Group>();

                    foreach (Group group in list)
                    {
                        _group.Add(group);
                    }
                }
                return _group;
            }
        }

        private static List<Student> _student;

        public static List<Student> addStudent
        {
            get
            {
                if (_student == null)
                {
                    var list = new Student[]
                    {
                        new Student { GroupId=1, FirstName="Max", LastName="Ivanov"},
                        new Student { GroupId=1, FirstName="Petr", LastName="Ivanov"},
                        new Student { GroupId=2, FirstName="Boris", LastName="Pupkin"},
                        new Student { GroupId=2, FirstName="Egor", LastName="Pupkin"},
                        new Student { GroupId=3, FirstName="Ivan", LastName="Petrov"},
                        new Student { GroupId=3, FirstName="Mitya", LastName="Bregnev"},
                        new Student { GroupId=4, FirstName="Masha", LastName="Gyrina"},
                        new Student { GroupId=4, FirstName="Anna", LastName="Kazyka"},
                        new Student { GroupId=5, FirstName="Kate", LastName="Smit"},
                        new Student { GroupId=5, FirstName="Jake", LastName="Ivanov"},
                        new Student { GroupId=6, FirstName="Slava", LastName="Makarov"},
                        new Student { GroupId=6, FirstName="Valera", LastName="Bespal"},
                    };

                    _student = new List<Student>();

                    foreach (Student student in list)
                    {
                        _student.Add(student);
                    }
                }
                return _student;
            }
        }
    }
}
