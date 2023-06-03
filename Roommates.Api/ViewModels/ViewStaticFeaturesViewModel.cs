using Roommates.Api.ViewModels.Base;
using Roommates.Infrastructure.Enums;

namespace Roommates.Api.ViewModels
{
    public class ViewStaticFeaturesViewModel : BaseViewModel
    {
        public decimal Price { get; set; }

        public short RoomsCount { get; set; }

        public bool IsForSelling { get; set; }

        public ClientType PreferedClientType { get; set; }

        public PricePeriodType? PricePeriodType { get; set; }

        public CurrencyType CurrencyType { get; set; }
    }
}
