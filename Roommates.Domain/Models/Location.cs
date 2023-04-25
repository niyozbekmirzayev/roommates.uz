using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Roommates;
using Roommates.Infrastructure.Base;
using Roommates.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Infrastructure.Models
{
    public class Location : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }
    }
}
