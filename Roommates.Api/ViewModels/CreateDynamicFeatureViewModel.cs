using Roommates.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.ViewModels
{
    public class CreateDynamicFeatureViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DynamicFeatureType DynamicFeatureType { get; set; }

        public long? Count { get; set; }

        public bool? IsExist { get; set; }
    }
}
