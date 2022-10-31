using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Mapsui.UI.Forms;
using Newtonsoft.Json;

namespace MyExpenses.Utils.Function.WebApi;

public partial class Nominatim : Http
{
    //todo application name
    private static HttpClient HttpClient { get; } = GetHttpClient("C#", "https://nominatim.openstreetmap.org");
    
    public static Position AddressToPoint(string address) => _AddressToPosition(address).Result;
    public static string? PointToAddress(Position position) => _PositionToAddress(position).Result;
    private static async Task<Position> _AddressToPosition(string address)
    {
        var latitude = 0f;
        var longitude = 0f;
        try
        {
            var httpResult = await HttpClient.GetAsync($"search?q={ParseToUrlFormat(address)}&format=json&polygon=1&addressdetails=1").ConfigureAwait(false);
            var result = await httpResult.Content.ReadAsStringAsync();
        
            var r = JsonConvert.DeserializeObject<List<NominatimStruc>>(result);
            latitude = r![0].lat;
            longitude = r[0].lon;
        }
        catch (Exception)
        {
            // ignored
        }
        return new Position (latitude, longitude);
    }
    
    private static async Task<string?> _PositionToAddress(Position position)
    {
        var r = new NominatimStruc();
        try
        {
            var httpResult = await HttpClient.GetAsync($"reverse?format=json&lat={position.Latitude.ToString(CultureInfo.InvariantCulture)}&lon={position.Longitude.ToString(CultureInfo.InvariantCulture)}").ConfigureAwait(false);
            var result = await httpResult.Content.ReadAsStringAsync();
            r = JsonConvert.DeserializeObject<NominatimStruc>(result);
        }
        catch (Exception)
        {
            // ignored
        }

        return r.display_name;
    }
}