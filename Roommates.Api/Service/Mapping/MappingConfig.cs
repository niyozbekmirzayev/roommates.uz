using AutoMapper;
using Roommates.Api.Service.ViewModels.IdentityService;
using Roommates.Api.Service.ViewModels.PostService;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Service.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateUserViewModel, User>().ReverseMap();
            CreateMap<CreatePostViewModel, Post>().ReverseMap();
        }
    }
}
