using System.ComponentModel.DataAnnotations;

namespace Roommates.Api.ViewModels
{
    public class CreateLocationViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
