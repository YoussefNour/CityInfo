using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.models
{
    public class PointOfInterestDto
    {
        public int Id { get; set; }
        public String Name { get; set; } = String.Empty;
        public String? Description { get; set; }
    }
}
