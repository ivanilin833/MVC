using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task9.Domain;
using Task9.Infrastucture;

namespace Task9.SqlRepository
{
    public class GroupRepository : IGroup
    {
        private readonly AppDBContext _appDBContent;
        private readonly IMapper _mapper;

        public GroupRepository(AppDBContext appDBContent, IMapper mapper)
        {
            _appDBContent = appDBContent;
            _mapper = mapper;
        }

        public async Task<Group> Add(Group t)
        {
            await _appDBContent.Groups.AddAsync(t);
            await _appDBContent.SaveChangesAsync();

            return _mapper.Map<Group>(t);
        }

        public async Task Delete(Group t)
        {
            _appDBContent.Groups.Remove(t);
            await _appDBContent.SaveChangesAsync();
        }

        public async Task<Group> Get(int id)
        {
            var groups = await _appDBContent.Groups.Include(c => c.Students).Where(c => c.GroupId == id).FirstOrDefaultAsync();

            return _mapper.Map<Group>(groups);
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            var group = await _appDBContent.Groups.Include(c => c.Courses).Include(c => c.Students).ToListAsync();

            return _mapper.Map<IEnumerable<Group>>(group);
        }

        public async Task Update(int id, Group t)
        {
            var group = await Get(id);
            group.Name = t.Name;
            await _appDBContent.SaveChangesAsync();
        }
    }
}
