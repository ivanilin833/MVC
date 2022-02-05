using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task9.Domain;
using Task9.Infrastucture;

namespace Task9.SqlRepository
{
    public class StudentRepository : IStudent
    {
        private readonly AppDBContext _appDBContent;
        private readonly IMapper _mapper;

        public StudentRepository(AppDBContext appDBContent, IMapper mapper)
        {
            _appDBContent = appDBContent;
            _mapper = mapper;
        }

        public async Task<Student> Add(Student t)
        {
            await _appDBContent.Students.AddAsync(t);
            await _appDBContent.SaveChangesAsync();

            return _mapper.Map<Student>(t);
        }

        public async Task Delete(Student t)
        {
            _appDBContent.Students.Remove(t);
            await _appDBContent.SaveChangesAsync();
        }

        public async Task<Student> Get(int id)
        {
            var student = await (from stud in _appDBContent.Students
                                 where stud.StudentId == id
                                 select stud).FirstOrDefaultAsync();

            return _mapper.Map<Student>(student);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var students = await _appDBContent.Students.Include(c => c.Group).ToListAsync();

            return _mapper.Map<IEnumerable<Student>>(students);
        }

        public async Task Update(int id, Student t)
        {
            var studen = await Get(id);
            studen.FirstName = t.FirstName;
            studen.LastName = t.LastName;
            _appDBContent.SaveChanges();
        }
    }
}
