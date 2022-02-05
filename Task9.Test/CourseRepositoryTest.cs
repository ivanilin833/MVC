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
    public class TestCourseRepository
    {
        [Theory]
        [InlineData(1, "Нельзя удалить курс в котором есть группы ")]
        [InlineData(2, "Нельзя удалить курс в котором есть группы ")]
        [InlineData(3, "Курс Chimia успешно удалён")]
        public async Task TestDeleteCourse(int id, string message)
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryCourses = new CourseRepository(GetMockContext(), mapper);
            var courseService = new CourseService(repositoryCourses);
            //Act
            var messageDelete = await courseService.Delete(id);

            //Assert
            Assert.Equal(message, messageDelete);
        }

        [Fact]
        public async Task TestAddCourse()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryCourses = new CourseRepository(GetMockContext(), mapper);
            var courseService = new CourseService(repositoryCourses);
            var testCourse = new Course { Name = "testCourse", Description = "This course for testing" };

            //Act
            var course = await courseService.Add(testCourse);

            //Assert
            Assert.Equal(course, testCourse);
        }

        [Fact]
        public async Task TestGetCourse()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryCourses = new CourseRepository(GetMockContext(), mapper);
            var courseService = new CourseService(repositoryCourses);
            var testCourse = GetMockContext().Courses.FirstOrDefault();

            //Act
            var course = await courseService.Get(1);

            //Assert
            Assert.Equal(testCourse.Name, course.Name);
        }

        [Fact]
        public async Task TestGetAllCourses()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryCourses = new CourseRepository(GetMockContext(), mapper);
            var courseService = new CourseService(repositoryCourses);

            //Act
            var countCourses = await courseService.GetAll();

            //Assert
            Assert.Equal(3, countCourses.Count());
        }

        [Fact]
        public async Task TestUpdateCourses()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryCourses = new CourseRepository(GetMockContext(), mapper);
            var courseService = new CourseService(repositoryCourses);
            var testCourses = new Course { Name = "Test", Description = "Test" };

            //Act
            await courseService.Update(1, testCourses);
            var courses = await courseService.Get(1);
            //Assert
            Assert.Equal(testCourses.Name, courses.Name);
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

            context.SaveChanges();

            return context;
        }
    }
}
