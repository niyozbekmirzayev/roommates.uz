using Roommates.Api.Service.ViewModels.Common;
using Roommates.Api.Service.ViewModels.FileService;
using Roommates.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.Service.ViewModels.PostService
{
    public class CreatePostViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public LocationViewModel Location { get; set; }

        public string Address { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public short RoomsCount { get; set; }

        bool IsForSelling { get; set; } = false;

        public Gender PreferedUserGender { get; set; } = Gender.NotSpecified;

        public PricePeriodType PricePeriodType { get; set; } = PricePeriodType.Monthly;

        [Required]
        public CurrencyType CurrencyType { get; set; }

        public ICollection<SaveFileViewModel> AppartmentViewFiles { get; set; }
    }
}
