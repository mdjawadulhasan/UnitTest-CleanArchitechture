using Application.Common.Shared;
using AutoMapper;
using Domain.ChildEntity;
using Domain.PersonEntity;

namespace Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonCreateUpdateDto>();
            CreateMap<PersonCreateUpdateDto, Person>();
            CreateMap<ChildCreateUpdateDto, Child>();
            CreateMap<Child, ChildCreateUpdateDto>();
        }
    }
}
