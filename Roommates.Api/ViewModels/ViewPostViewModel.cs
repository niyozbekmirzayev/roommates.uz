using Roommates.Api.ViewModels.Base;
using Roommates.Infrastructure.Enums;

namespace Roommates.Api.ViewModels
{
    public class ViewPostViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        PostType PostType { get; set; }

        public ViewLocationViewModel Location { get; set; }

        public ViewStaticFeaturesViewModel StaticFeatures { get; set; }

        public List<ViewDynamicFeatureViewModel> DynamicFeatures { get; set; }

        public List<GetFileViewModel> AppartmentViewFiles { get; set; }

        public long ViewsCount { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisliked { get; set; }

        public PreviewUserViewModel Author { get; set; }
    }
}
