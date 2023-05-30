using Newtonsoft.Json;
using Roommates.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.ViewModels
{
    public class StaticFeaturesViewModel
    {
        [Required]
        PostType PostType { get; set; } = PostType.Apartment;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short RoomsCount { get; set; }

        [Required]
        public bool IsForSelling { get; set; } = false;

        [Required]
        public ClientType PreferedClientType { get; set; } = ClientType.All;

        public PricePeriodType? PricePeriodType { get; set; }

        [Required]
        public CurrencyType CurrencyType { get; set; }
    }
}
