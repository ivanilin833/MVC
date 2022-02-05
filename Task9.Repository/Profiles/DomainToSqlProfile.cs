using AutoMapper;
using Task9.SqlRepository.Models;

namespace Task9.SqlRepository.Profiles
{
    public class DomainToSqlProfile : Profile
    {
        public DomainToSqlProfile()
        {
            CreateMap<Domain.Course, Course>();

            CreateMap<Domain.Group, Group>();

            CreateMap<Domain.Student, Student>();
        }
    }
}