using Roommates.Api.Service.ViewModels.Base;
using Roommates.Infrastructure.Enums;

namespace Roommates.Api.Service.ViewModels
{
    public class ViewPostViewModel : BaseViewModel
    {
        public string Title { get; set; }

        PostType PostType { get; set; }

        public string Description { get; set; }

        public Guid LocationId { get; set; }

        public string Address { get; set; }

        public decimal Price { get; set; }

        public short RoomsCount { get; set; }

        public bool IsForSelling { get; set; }

        public long ViewedCount { get; set; }

        public Gender PreferedUserGender { get; set; }

        public PricePeriodType? PricePeriodType { get; set; }

        public CurrencyType CurrencyType { get; set; }

        public Guid CreatedByUserId { get; set; }

        public List<GetFileViewModel> AppartmentViewFiles { get; set; }

        public bool IsLiked { get; set; }

        public bool IsDisliked { get; set; }

        public LocationViewModel Location { get; set; }

        public PreviewUserViewModel AuthorUser { get; set; }

    }
}
