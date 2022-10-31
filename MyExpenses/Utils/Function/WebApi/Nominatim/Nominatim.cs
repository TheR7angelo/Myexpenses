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
    private static string UserAgent { get; set; } = "C#";
    private static HttpClient HttpClient { get; } = GetHttpClient(UserAgent, "https://nominatim.openstreetmap.org");
    public Nominatim(string userAgent)
    {
        UserAgent = userAgent;
    }

    public List<NominatimStruc>? AddressToNominatim(string address) => _AddressToNominatim(address).Result;
    public NominatimStruc? PointToNominatim(Position position) => _PositionToNominatim(position).Result;
    private static async Task<List<NominatimStruc>?> _AddressToNominatim(string address)
    {
        try
        {
            var httpResult = await HttpClient.GetAsync($"search?q={ParseToUrlFormat(address)}&format=json&polygon=1&addressdetails=1").ConfigureAwait(false);
            var result = await httpResult.Content.ReadAsStringAsync();
        
            return JsonConvert.DeserializeObject<List<NominatimStruc>>(result);
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    private static async Task<NominatimStruc?> _PositionToNominatim(Position position)
    {
        try
        {
            var httpResult = await HttpClient.GetAsync($"reverse?format=json&lat={position.Latitude.ToString(CultureInfo.InvariantCulture)}&lon={position.Longitude.ToString(CultureInfo.InvariantCulture)}").ConfigureAwait(false);
            var result = await httpResult.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<NominatimStruc>(result);
        }
        catch (Exception)
        {
            return null;
        }
    }
}