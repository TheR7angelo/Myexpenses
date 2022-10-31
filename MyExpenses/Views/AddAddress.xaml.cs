using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.Tiling;
using Mapsui.UI.Forms;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
using MyExpenses.Utils.Function.WebApi;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace MyExpenses.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class AddAddress
{
    private static readonly Nominatim Nominatim = new();
    public AddAddress()
    {
        //todo finir
        InitializeComponent();

        Ui();
    }

    private async void Ui()
    {
        var map = new Mapsui.Map { CRS = "EPSG:4326" };
        var tileLayer = OpenStreetMap.CreateTileLayer();
        map.Layers.Add(tileLayer);

        map.Widgets.Add(new ScaleBarWidget(map)
        {
            TextAlignment = Alignment.Center,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom
        });
        MapView.Map = map;
        
        var location = await GetCurrentLocation();
        if (location is null) return;
        var smc = SphericalMercator.FromLonLat(location.Longitude, location.Latitude);
        var position = new Position(location.Latitude, location.Longitude);
            
        MapView.MyLocationLayer.UpdateMyLocation(position);
        MapView.Navigator!.NavigateTo(new MPoint(smc.x, smc.y), MapView.Map.Resolutions[17]);
    }

    private static async Task<Location?> GetCurrentLocation()
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

    private void MapView_OnMapClicked(object sender, MapClickedEventArgs e)
    {
        UpdatePosition(e.Point);
    }

    private void UpdatePosition(Position position)
    {
        Task.Run(() =>
        {
            MapView.Pins.Clear();
            MapView.Pins.Add(new Pin(new MapView())
            {
                Position = position,
                Type = PinType.Pin
            });
        });

        EditorLatitude.Text = position.Latitude.ToString(CultureInfo.InvariantCulture);
        EditorLongitude.Text = position.Longitude.ToString(CultureInfo.InvariantCulture);
        
        var nums = string.Empty;
        var cityName = string.Empty;
        var postal = string.Empty;
        var country = string.Empty;
        var countryCode = string.Empty;
        
        var address = Nominatim.PointToAddress(position);
        if (address is not null)
        {
            nums = address.Value.address.house_number;
            cityName = address.Value.address.city ?? address.Value.address.village;
            postal = address.Value.address.postcode.ToString();
            country = address.Value.address.country;
            countryCode = address.Value.address.country_code;
        }

        EditorNums.Text = nums;
        EditorCityName.Text = cityName;
        EditorCityPostal.Text = postal;
        EditorCityCountry.Text = country;
        EditorCityCountryCode.Text = countryCode;
    }

    private void EditorAddress_OnCompleted(object sender, EventArgs e)
    {
        var nums = ParseToEmpty(EditorNums.Text);
        var cityName = ParseToEmpty(EditorCityName.Text);
        var postal = ParseToEmpty(EditorCityPostal.Text);
        var country =  ParseToEmpty(EditorCityCountry.Text);
    }

    private void EditorCoord_OnCompleted(object sender, EventArgs e)
    {
        var latitude = ParseToEmpty(EditorLatitude.Text).Replace(',', '.');
        var longitude = ParseToEmpty(EditorLongitude.Text).Replace(',', '.');
    }

    private static string ParseToEmpty(string? str) => str ?? string.Empty;
}