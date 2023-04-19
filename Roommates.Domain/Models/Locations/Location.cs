using Newtonsoft.Json.Converters;
using Roommates.Domain.Base;
using Roommates.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Roommates.Domain.Models.Locations
{
    public class Location : BaseModel, IPersistentEntity
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityState EntityState { get; set; } = EntityState.Active;
    }
}
