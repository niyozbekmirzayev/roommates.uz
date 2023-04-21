using AutoMapper;
using Roommates.Domain.Models.Roommates;
using Roommates.Service.ViewModels;

namespace Roommates.Service.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateUserViewModel, User>().ReverseMap();
        }
    }
}
