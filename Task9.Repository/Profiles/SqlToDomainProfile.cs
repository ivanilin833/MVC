using AutoMapper;
using Task9.SqlRepository.Models;

namespace Task9.SqlRepository.Profiles
{
    public class SqlToDomainProfile : Profile
    {
        public SqlToDomainProfile()
        {
            CreateMap<Course, Domain.Course>();
            CreateMap<Group, Domain.Group>();
            CreateMap<Student, Domain.Student>();
        }
    }
}