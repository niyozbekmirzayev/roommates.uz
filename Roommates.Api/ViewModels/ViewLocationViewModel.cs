using Roommates.Api.ViewModels.Base;

namespace Roommates.Api.ViewModels
{
    public class ViewLocationViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
