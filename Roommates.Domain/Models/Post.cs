using Newtonsoft.Json.Converters;
using Roommates.Domain.Base;
using Roommates.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Roommates.Domain.Models
{
    public class Post : BaseModel
    {
        [Required]
        public string Title { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        PostType PostType { get; set; } = PostType.Apartment;

        [Required]
        public string Description { get; set; }

        public string? Location { get; set; }
        
        public decimal? Price { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public PriceType? PriceType { get; set; }

        public CurrencyType? CurrencyType { get; set; }

        public List<Media>? AppartmentPhotos { get; set; }
    }
}
