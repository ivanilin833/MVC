using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Task9.Application;
using Task9.Domain;
using Task9.SqlRepository;
using Xunit;

namespace Task9.Test
{
    public class TestStudentRepository
    {
        [Fact]
        public async Task TestDeleteStudent()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryStudent = new StudentRepository(GetMockContext(), mapper);
            var studentService =  new StudentService(repositoryStudent);
            var studen = await studentService.Get(1);
            //Act
            var messageDelete = await studentService.Delete(1);

            //Assert
            Assert.Equal(String.Format("Cтудент {0} {1} успешно удалён", studen.FirstName, studen.LastName), messageDelete);
        }

        [Fact]
        public async Task TestAddStudent()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryStudent = new StudentRepository(GetMockContext(), mapper);
            var studentService = new StudentService(repositoryStudent);
            var testStudent = new Student { FirstName = "testFirstName", LastName = "testLastName" };

            //Act
            var students = await studentService.Add(testStudent);

            //Assert
            Assert.Equal(students, testStudent);
        }

        [Fact]
        public async Task TestGetStudent()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryStudent = new StudentRepository(GetMockContext(), mapper);
            var studentService = new StudentService(repositoryStudent);
            var testStudent = GetMockContext().Students.FirstOrDefault();

            //Act
            var students = await studentService.Get(1);

            //Assert
            Assert.Equal(testStudent.FirstName, students.FirstName);
        }

        [Fact]
        public async Task TestGetAllStudent()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryStudent = new StudentRepository(GetMockContext(), mapper);
            var studentService = new StudentService(repositoryStudent);

            //Act
            var countStudent = await studentService.GetAll();

            //Assert
            Assert.Equal(4, countStudent.Count());
        }

        [Fact]
        public async Task TestUpdateStudent()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryStudent = new StudentRepository(GetMockContext(), mapper);
            var studentService = new StudentService(repositoryStudent);
            var testStudent = new Student { FirstName = "testFirstName", LastName = "testLastName" };

            //Act
            await studentService.Update(1, testStudent);
            var student = await studentService.Get(1);
            //Assert
            Assert.Equal(testStudent.FirstName, student.FirstName);
            Assert.Equal(testStudent.LastName, student.LastName);
        }

        private AppDBContext GetMockContext()
        {
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new AppDBContext(options);

            context.Courses.Add(new Course { Name = "Math", Description = "Course for Math" });
            context.Courses.Add(new Course { Name = "Biologic", Description = "Course for Biologic" });
            context.Courses.Add(new Course { Name = "Chimia", Description = "Course for Chimia" });

            context.Groups.Add(new Group { CourseId = 1, Name = "A1" });
            context.Groups.Add(new Group { CourseId = 1, Name = "A2" });
            context.Groups.Add(new Group { CourseId = 2, Name = "B1" });

            context.Students.Add(new Student { GroupId = 1, FirstName = "Max", LastName = "Ivanov" });
            context.Students.Add(new Student { GroupId = 1, FirstName = "Petr", LastName = "Ivanov" });
            context.Students.Add(new Student { GroupId = 2, FirstName = "Boris", LastName = "Pupkin" });
            context.Students.Add(new Student { GroupId = 2, FirstName = "Egor", LastName = "Pupkin" });

            context.SaveChanges();

            return context;
        }
    }
}
