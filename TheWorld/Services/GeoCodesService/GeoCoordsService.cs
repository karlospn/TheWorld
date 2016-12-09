using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace TheWorld.Services.GeoCodesService
{
    public class GeoCoordsService : IGeoCoordsService
    {
        private readonly IConfigurationRoot _configuration;

        public GeoCoordsService(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public async Task<GeoCoordsServiceModel> GetCoordsAsync(string name)
        {
            var result = new GeoCoordsServiceModel();
            var apiKey = _configuration["Keys:BingMapsAPIKey"];
            var encodedName = WebUtility.UrlEncode(name);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodedName}&key={apiKey}";

            var client = new HttpClient();

            var json = await client.GetStringAsync(url);

            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!results["resourceSets"][0]["resources"].HasValues)
            {
                result.Message = $"Could not find '{name}' as a location";
            }
            else
            {
                var confidence = (string) resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{name}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double) coords[0];
                    result.Longitude = (double) coords[1];
                    result.Message = "Success";
                    result.Success = true;

                }
            }
            return result;
        }
    }
}
