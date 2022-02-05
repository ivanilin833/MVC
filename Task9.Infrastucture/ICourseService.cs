using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Domain;

namespace Task9.Infrastucture
{
    public interface ICourseService 
    {
        Task<Course> Add(Course t);
        Task Update(int id, Course t);
        Task<string> Delete(int id);
        Task<Course> Get(int id);
        Task<IEnumerable<Course>> GetAll();
    }
}
