using Roommates.Api.ViewModels.Base;
using Roommates.Infrastructure.Enums;

namespace Roommates.Api.ViewModels
{
    public class ViewDynamicFeatureViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public DynamicFeatureType DynamicFeatureType { get; set; }

        public long? Count { get; set; }

        public bool? IsExist { get; set; }
    }
}
