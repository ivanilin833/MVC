using System.Collections.Generic;
using System.Threading.Tasks;

namespace Task9.Infrastucture
{
    public interface IRepository<T>
    {
        Task<T> Add(T t);
        Task Update(int id, T t);
        Task Delete(T t);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
    }
}
