using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyExpenses.Utils.Function;

public static class Essential
{
    public static async Task<Location?> GetCurrentLocation()
    {
        try
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var cts = new CancellationToken();

            var location = await Geolocation.GetLocationAsync(request, cts);
            return location;
        }
        catch (Exception) 
        {
            return null;
        }
    }
}