using Task9.Domain;
using Task9.Infrastucture;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Task9.Application
{
    public class CourseService : ICourseService
    {
        private readonly ICourse _course;

        public CourseService(ICourse course)
        {
            _course = course;
        }

        public async Task<Course> Add(Course t)
        {
            return await _course.Add(t);
        }

        public async Task<string> Delete(int id)
        {
            var course = await _course.Get(id);
            try
            {
                if (course.Group.Count != 0)
                {
                    throw new Exception("Нельзя удалить курс в котором есть группы ");
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            string messageDelete = String.Format("Курс {0} успешно удалён", course.Name);
            await _course.Delete(course);
            return messageDelete;
        }

        public async Task<Course> Get(int id)
        {

            return await _course.Get(id);
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            return await _course.GetAll();
        }

        public async Task Update(int id, Course t)
        {
            await _course.Update(id, t);
        }
    }
}
