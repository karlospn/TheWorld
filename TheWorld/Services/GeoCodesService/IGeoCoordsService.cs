using System.Threading.Tasks;

namespace TheWorld.Services.GeoCodesService
{
    public interface IGeoCoordsService
    {
        Task<GeoCoordsServiceModel> GetCoordsAsync(string name);
    }
}