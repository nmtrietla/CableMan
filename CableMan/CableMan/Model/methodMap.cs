using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace CableMan.Model
{
    public static class MethodMap
    {
        public static async Task<Position> GetLoCationNow()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync(10000);
                return new Position(position.Latitude, position.Longitude);
            }
            catch
            {
                throw;
            }
            
        }
    }
}
