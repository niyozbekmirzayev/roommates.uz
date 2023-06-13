using Newtonsoft.Json;
using Roommates.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public PostType PostType { get; set; } = PostType.Apartment;

        [Required]
        public CreateLocationViewModel Location { get; set; }

        [Required]
        public CreateStaticFeaturesViewModel StaticFeatures { get; set; }

        public ICollection<CreateDynamicFeatureViewModel> DynamicFeatures { get; set; }

        public ICollection<SaveFileViewModel> AppartmentViewFiles { get; set; }
    }
}
