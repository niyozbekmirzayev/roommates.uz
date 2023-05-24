using AutoMapper;
using Roommates.Api.Service.ViewModels;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Service.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateUserViewModel, User>().ReverseMap();
            CreateMap<CreatePostViewModel, Post>()
                .ForMember(dest => dest.AppartmentViewFiles, opt => opt.Ignore())
                .ReverseMap();
            
            CreateMap<Location, LocationViewModel>().ReverseMap();
            CreateMap<GetFileViewModel, FilePost>().ReverseMap();
            CreateMap<User, PreviewUserViewModel>().ReverseMap();

            CreateMap<Post, ViewPostViewModel> ()
                .ForMember(des => des.AuthorUser, opt => opt.MapFrom(src => src.CreatedByUser))
                .ForMember(des => des.AppartmentViewFiles, opt => opt.MapFrom(src => src.AppartmentViewFiles))
                .ReverseMap();
        }
    }
}
