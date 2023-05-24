using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.Service.ViewModels
{
    public class LocationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
