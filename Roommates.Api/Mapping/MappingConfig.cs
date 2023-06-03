using AutoMapper;
using Roommates.Api.ViewModels;
using Roommates.Infrastructure.Models;

namespace Roommates.Api.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, CreateUserViewModel>().ReverseMap();
            CreateMap<Post, CreatePostViewModel>().ReverseMap()
                .ForMember(dest => dest.AppartmentViewFiles, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Location, CreateLocationViewModel>().ReverseMap();
            CreateMap<Location, ViewLocationViewModel>().ReverseMap();

            CreateMap<FilePost, GetFileViewModel>().ReverseMap();
            CreateMap<User, PreviewUserViewModel>().ReverseMap();

            CreateMap<DynamicFeature, CreateDynamicFeatureViewModel>().ReverseMap();
            CreateMap<StaticFeatures, CreateStaticFeaturesViewModel>().ReverseMap();

            CreateMap<StaticFeatures, ViewStaticFeaturesViewModel>().ReverseMap();
            CreateMap<DynamicFeature, ViewDynamicFeatureViewModel>().ReverseMap();

            CreateMap<Post, ViewPostViewModel>().ReverseMap();
        }
    }
}
