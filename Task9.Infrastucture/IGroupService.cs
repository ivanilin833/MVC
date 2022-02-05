using System.Collections.Generic;
using System.Threading.Tasks;
using Task9.Domain;

namespace Task9.Infrastucture
{
    public interface IGroupService 
    {
        Task<Group> Add(Group t);
        Task Update(int id, Group t);
        Task<string> Delete(int id);
        Task<Group> Get(int id);
        Task<IEnumerable<Group>> GetAll();
    }
}
