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
    public class TestGroupRepository
    {
        [Theory]
        [InlineData(1, "Нельзя удалить группу в которой есть студенты")]
        [InlineData(2, "Нельзя удалить группу в которой есть студенты")]
        [InlineData(3, "Группа B1 успешно удалена")]
        public async Task TestDeleteGroup(int id, string message)
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryGroup = new GroupRepository(GetMockContext(), mapper);
            var groupService = new GroupService(repositoryGroup);

            //Act
            var messageDelete = await groupService.Delete(id);

            //Assert
            Assert.Equal(message, messageDelete);
        }

        [Fact]
        public async Task TestAddGroup()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryGroup = new GroupRepository(GetMockContext(), mapper);
            var groupService = new GroupService(repositoryGroup);
            var testGroup = new Group { Name = "testGroup" };

            //Act
            var groups = await groupService.Add(testGroup);

            //Assert
            Assert.Equal(groups, testGroup);
        }

        [Fact]
        public async Task TestGetGroup()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryGroup = new GroupRepository(GetMockContext(), mapper);
            var groupService = new GroupService(repositoryGroup);
            var testGroup = GetMockContext().Groups.FirstOrDefault();

            //Act
            var groups = await groupService.Get(1);

            //Assert
            Assert.Equal(testGroup.Name, groups.Name);
        }

        [Fact]
        public async Task TestGetAllGroup()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryGroup = new GroupRepository(GetMockContext(), mapper);
            var groupService = new GroupService(repositoryGroup);

            //Act
            var countGroup = await groupService.GetAll();

            //Assert
            Assert.Equal(3, countGroup.Count());
        }

        [Fact]
        public async Task TestUpdateGroup()
        {
            //Arrange
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SqlRepository.Profiles.DomainToSqlProfile());
            });
            var mapper = config.CreateMapper();
            var repositoryGroup = new GroupRepository(GetMockContext(), mapper);
            var groupService = new GroupService(repositoryGroup);
            var testGroup = new Group() { Name = "Test" };

            //Act
            await groupService.Update(1, testGroup);
            var groups = await groupService.Get(1);
            //Assert
            Assert.Equal(testGroup.Name, groups.Name);
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
