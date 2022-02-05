using Task9.Domain;
using Task9.Infrastucture;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Task9.Application
{
    public class StudentService : IStudentService
    {
        private readonly IStudent _student;

        public StudentService(IStudent student)
        {
            _student = student;
        }

        public async Task<Student> Add(Student t)
        {
            return await _student.Add(t);
        }

        public async Task<string> Delete(int id)
        {
            var student = await Get(id);
            string messageDelete = String.Format("Cтудент {0} {1} успешно удалён", student.FirstName, student.LastName);
            await _student.Delete(student);

            return messageDelete;
        }

        public async Task<Student> Get(int id)
        {

            return await _student.Get(id);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _student.GetAll();
        }

        public async Task Update(int id, Student t)
        {
            await _student.Update(id, t);
        }
    }
}
