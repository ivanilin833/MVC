using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Domain;

namespace Task9.Infrastucture
{
    public interface IStudentService 
    {
        Task<Student> Add(Student t);
        Task Update(int id, Student t);
        Task<string> Delete(int id);
        Task<Student> Get(int id);
        Task<IEnumerable<Student>> GetAll();
    }
}

