using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Roommates.Domain.Base;
using Roommates.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Domain.Models.Locations
{
    public class Location : BaseModel, IPersistentEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public EntityState EntityState { get; set; } = EntityState.Active;
    }
}
