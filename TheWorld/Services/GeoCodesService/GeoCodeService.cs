using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Services.GeoCodesService
{
    public class GeoCoordsServiceModel
    {
        public string Message { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool Success { get; set; } = false;
    }
}
