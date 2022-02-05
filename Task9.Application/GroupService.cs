using Task9.Domain;
using Task9.Infrastucture;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Task9.Application
{
    public class GroupService : IGroupService
    {
        private readonly IGroup _group;

        public GroupService(IGroup group)
        {
            _group = group;
        }

        public async Task<Group> Add(Group t)
        {
            return await _group.Add(t);
        }

        public async Task<string> Delete(int id)
        {
            var group = await _group.Get(id);

            try
            {
                if (group.Students.Count != 0)
                {
                    throw new Exception("Нельзя удалить группу в которой есть студенты");
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            string messageDelete = String.Format("Группа {0} успешно удалена", group.Name);
            await _group.Delete(group);
            return messageDelete;
        }

        public async Task<Group> Get(int id)
        {
            return await _group.Get(id);
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            return await _group.GetAll();
        }

        public async Task Update(int id, Group t)
        {
            await _group.Update(id, t);
        }
    }
}
