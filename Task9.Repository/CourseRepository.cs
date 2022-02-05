using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task9.Domain;
using Task9.Infrastucture;

namespace Task9.SqlRepository
{
    public class CourseRepository : ICourse
    {
        private readonly AppDBContext _appDBContent;
        private readonly IMapper _mapper;

        public CourseRepository(AppDBContext appDBContent, IMapper mapper)
        {
            _appDBContent = appDBContent;
            _mapper = mapper;
        }

        public async Task<Course> Add(Course t)
        {
            await _appDBContent.Courses.AddAsync(t);
            await _appDBContent.SaveChangesAsync();

            return _mapper.Map<Course>(t);
        }

        public async Task Delete(Course t)
        {
            _appDBContent.Courses.Remove(t);
            await _appDBContent.SaveChangesAsync();
        }

        public async Task<Course> Get(int id)
        {
            var courses = await _appDBContent.Courses.Include(c => c.Group).Where(c => c.CourseId == id).FirstOrDefaultAsync();

            return _mapper.Map<Course>(courses);
        }

        public async Task<IEnumerable<Course>> GetAll()
        {
            IEnumerable<Course> courses = await _appDBContent.Courses.Include(c => c.Group).ThenInclude(p => p.Students).ToListAsync();

            return _mapper.Map<IEnumerable<Course>>(courses);
        }

        public async Task Update(int id, Course t)
        {
            var cours = await Get(id);
            cours.Name = t.Name;
            await _appDBContent.SaveChangesAsync();
        }
    }
}
